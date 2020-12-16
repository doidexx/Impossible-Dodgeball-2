using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasName
{
    Main,
    Skins,
    Pause,
    Records,
    GameOver,
    Game
}

public class CanvasS : MonoBehaviour
{
    public CanvasName canvasName;
}
