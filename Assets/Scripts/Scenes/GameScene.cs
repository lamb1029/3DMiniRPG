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

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        //for (int i = 0; i < 5; i++)
        //    Managers.Resource.Instantiate("Player");

        //co = StartCoroutine("ExplodeAfterSecinds", 4.0f);

        //StopCoroutine(co);
    }

    public override void Clear()
    {

    }
}
