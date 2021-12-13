using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill
    }

    PlayerStat _stat;
    Vector3 _destPos;
    Animator anim;

    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    GameObject _lockTarget;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch(_state)
            {
                case PlayerState.Die:
                    anim.SetBool("attack", false);
                    break;
                case PlayerState.Idle:
                    anim.SetFloat("speed", 0);
                    anim.SetBool("attack", false);
                    break;
                case PlayerState.Moving:
                    anim.SetFloat("speed", _stat.MoveSpeed);
                    anim.SetBool("attack", false);
                    break;
                case PlayerState.Skill:
                    anim.SetBool("attack", true);
                    break;
            }
        }
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        anim = GetComponent<Animator>();
        //Manager.Input.KeyAction -= OnKeyboard;
        //Manager.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Update()
    {
        switch(State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;

        }
    }

    bool _stopSkill = false;
    private void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEbemt_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEbemt_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEbemt_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = PlayerState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        //몬스터가 사정거리안에 있으면 공격
        if(_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        //이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }


            //transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }
    }
    void UpdateIdle()
    {
    }
    
    void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        if (_stopSkill)
            State = PlayerState.Idle;
        else
            State = PlayerState.Skill;
    }
    #region 버림
    //void Update_01()
    //{
    //    //if(_moveToDest)
    //    //{
    //    //    Vector3 dir = _destPos - transform.position;
    //    //    if(dir.magnitude < 0.001f)
    //    //    {
    //    //        _moveToDest = false;
    //    //    }
    //    //    else
    //    //    {
    //    //        float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
    //    //        transform.position += dir.normalized * moveDist;

    //    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    //    //        transform.LookAt(_destPos);
    //    //    }
    //    //}

    //    //if (_moveToDest)
    //    //{
    //    //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
    //    //    ani.SetFloat("wait_run_ratio", wait_run_ratio);
    //    //    ani.Play("Wait_Run");
    //    //}
    //    //else
    //    //{
    //    //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
    //    //    ani.SetFloat("wait_run_ratio", wait_run_ratio);
    //    //    ani.Play("Wait_Run");
    //    //}
    //}

    //void OnKeyboard()
    //{
    //    // Local -> World
    //    // TransformDirection
    //    // World -> Local
    //    // InverseTransformDirection

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        //특정 방향으로 쳐다보기
    //        //transform.rotation = Quaternion.LookRotation(Vector3.forward);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
    //        //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
    //        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);
    //        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);
    //        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);
    //        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    //    }

    //    //_yAngle += Time.deltaTime * 100f;
    //    // transform.eulerAngles = new Vector3(0, _yAngle, 0);
    //    // transform.Rotate(new Vector3(0, Time.deltaTime * 100f, 0));

    //    //transform.rotation = Quaternion.Euler(new Vector3(0, _yAngle, 0));
    //    //_moveToDest = false;
    //}
    #endregion
}
