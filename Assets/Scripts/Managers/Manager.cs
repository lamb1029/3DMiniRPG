using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager s_instance;
    //public static Manager GetInstance() { Init(); return s_instance; }
    static Manager Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("Manager");
            if(go == null)
            {
                go = new GameObject { name = "Manager" };
                go.AddComponent<Manager>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Manager>();
        }
    }
}
