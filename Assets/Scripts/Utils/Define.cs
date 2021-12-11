using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }
    public enum UIEvent
    {
        Click,
        Drag
    }
    public enum CameraMode
    {
        QuarterView
    }

    public enum MouseEvent
    {
        Press,
        Click
    }
}