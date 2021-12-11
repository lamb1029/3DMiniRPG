using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip A1;
    public AudioClip A2;


    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        i++;

        if(i % 2 == 0)
            Managers.Sound.Play(A1, Define.Sound.Bgm);
        else
            Managers.Sound.Play(A2, Define.Sound.Bgm);
    }
}
