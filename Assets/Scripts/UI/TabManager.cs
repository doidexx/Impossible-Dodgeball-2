using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject skinWindow = null;
    public GameObject ballWindow = null;
    public CameraControl camControl = null;

    public void OpenSkinTab()
    {
        ballWindow.SetActive(false);
        skinWindow.SetActive(true);
        camControl.MoveCameraTo(1);
    }

    public void OpenBallTab()
    {
        ballWindow.SetActive(true);
        skinWindow.SetActive(false);
        camControl.MoveCameraTo(2);
    }
}
