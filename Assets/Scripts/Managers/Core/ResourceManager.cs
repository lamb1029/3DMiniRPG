using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        //original을 미이 들고 있다면 바로 사용
        GameObject orgonal = Load<GameObject>($"Prefabs/{path}");
        if(orgonal == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //혹시 폴링된 애가 있을까?
        if (orgonal.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(orgonal, parent).gameObject;

        GameObject go = Object.Instantiate(orgonal, parent);
        //int index = go.name.IndexOf("(Clone)");
        //if (index > 0)
        //    go.name = go.name.Substring(0, index);
        go.name = orgonal.name;

        return go;
    }

    public void Destroy(GameObject obj)
    {
        if (obj == null)
            return;

        Poolable poolable = obj.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(obj);
    }
}
