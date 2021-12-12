using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        //for (int i = 0; i < 5; i++)
        //    Managers.Resource.Instantiate("Player");

        //co = StartCoroutine("ExplodeAfterSecinds", 4.0f);

        //StopCoroutine(co);
    }

    //IEnumerable CoStopExplode(float seconds)
    //{

    //}

    //IEnumerable ExplodeAfterSecinds(float seconsd)
    //{
    //    yield return new WaitForSeconds(seconsd);
    //}

    public override void Clear()
    {

    }
}
