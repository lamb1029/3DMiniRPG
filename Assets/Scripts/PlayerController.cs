using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f,
          _yAngle;

    void Start()
    {
        Manager.Input.KeyAction -= OnKeyboard;
        Manager.Input.KeyAction += OnKeyboard;
    }

    void Update()
    {

    }

    void OnKeyboard()
    {
        // Local -> World
        // TransformDirection
        // World -> Local
        // InverseTransformDirection

        if (Input.GetKey(KeyCode.W))
        {
            //특정 방향으로 쳐다보기
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
            //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        _yAngle += Time.deltaTime * 100f;
        // transform.eulerAngles = new Vector3(0, _yAngle, 0);
        // transform.Rotate(new Vector3(0, Time.deltaTime * 100f, 0));

        //transform.rotation = Quaternion.Euler(new Vector3(0, _yAngle, 0));

    }
}
