using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 2;

    Vector3 zeropos; //생성될때 처음 위치
    bool isreturn = false;
    float area = 30f; //몬스터 영역

    protected override void Start()
    {
        base.Start();
        zeropos = transform.position;
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();
        anim = GetComponent<Animator>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        if(isreturn == false)
        {
            //플레이어 서칭
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject player = Managers.Game.GetPlayer();
            if (player == null)
                return;

            float distance = (player.transform.position - transform.position).magnitude;
            if(distance <= _scanRange)
            {
                _lockTarget = player;
                State = Define.State.Moving;
                return;
            }
        }
    }

    protected override void UpdateMoving()
    {
        if(isreturn == false)
        {
            //플레이어가 사정거리 안에 오면 공격
            if (_lockTarget != null)
            {
                _destPos = _lockTarget.transform.position;
                float distance = (_destPos - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                    nma.SetDestination(transform.position);
                    State = Define.State.Skill;
                    return;
                }
            }

            //이동
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.1f)
            {
                State = Define.State.Idle;
            }
            else
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(_destPos);
                nma.speed = _stat.MoveSpeed;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }
        
        MonsterReturn();
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);

            if(targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }

    void MonsterReturn()
    {
        NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
        float dir = (transform.position - zeropos).magnitude;

        //생성된 자리로 부터 멀어지면 다시 본래 자리로 돌아감
        if( dir > 35.0f)
        {
            isreturn = true;
            _lockTarget = null;
            State = Define.State.Moving;
            nma.SetDestination(zeropos);
        }
        //본래 자리로 돌아오면 다시 주변을 서칭
        if (dir <= 0.1f)
        {
            isreturn = false;
            State = Define.State.Idle;
        }

    }
}
