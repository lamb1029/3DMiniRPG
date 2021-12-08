using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(Input.mousePosition); //screen ��ǥ
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); //Viewport ��ǥ
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = (1 << 8) | (1 << 9);
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raucast Camera {hit.collider.gameObject.name}");
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousepos - Camera.main.transform.position;
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raucast Camera {hit.collider.gameObject.name}");
        //    }
        //}
    }

    //Vector3 look = transform.TransformDirection(Vector3.forward);
    //Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

    ////��Ÿ��� ��� ��ü�� ����
    //RaycastHit[] hits;
    //hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);
    //foreach (RaycastHit hit in hits)
    //{
    //    Debug.Log($"Raycast! {hit.collider.gameObject.name}");
    //}

    //���� ó���� �͸� ����
    //RaycastHit hit;
    //if(Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
    //{
    //    Debug.Log($"Raycast! {hit.collider.gameObject.name}");
    //}
}
