using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    //bool _moveToDest = false;
    Vector3 _destPos;
    Animator ani;
    public enum PlayerState
    {
        Die,
        Moving,
        Idle
    }
    PlayerState _state = PlayerState.Idle;

    void Start()
    {
        ani = GetComponent<Animator>();
        //Manager.Input.KeyAction -= OnKeyboard;
        //Manager.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        switch(_state)
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
        }
    }

    private void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        //if (evt != Define.MouseEvent.Click)
        //    return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        //int mask = (1 << 8) | (1 << 9);
        //LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            //_moveToDest = true;
            _state = PlayerState.Moving;
            //Debug.Log($"Raucast Camera {hit.collider.gameObject.name}");
        }
    }
    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }
        //���� ���� ���¿� ���� ������ �Ѱ���
        ani.SetFloat("speed", _speed);
    }
    void UpdateIdle()
    {
        ani.SetFloat("speed", 0);
    }
    
    #region ����
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
    //        //Ư�� �������� �Ĵٺ���
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