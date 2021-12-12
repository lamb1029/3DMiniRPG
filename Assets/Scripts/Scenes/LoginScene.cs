using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //SceneManager.LoadScene("Game");
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        //List<GameObject> list = new List<GameObject>();
        //for (int i = 0; i < 5; i++)
        //    list.Add(Managers.Resource.Instantiate("Player"));

        //foreach (GameObject obj in list)
        //{
        //    Managers.Resource.Destroy(obj);
        //}
    }

    public override void Clear()
    {

    }
}
