using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [Header("Virtual Cameras")]
    public CinemachineVirtualCamera homeVCAM = null;
    public CinemachineDollyCart VCamCart = null;
    public float VCamDesirePosition = 0;
    public float timeToReachPosition = 0.125f;
    [Header("VCam Rotations")]
    public Vector3 VCamDesiredRotation = Vector3.zero;
    public Vector3[] rotations = null;

    float VCamPos = 0;
    Vector3 VCamRotation = Vector3.zero;

    private void Update()
    {
        VCamPos = Mathf.Lerp(VCamPos, VCamDesirePosition, timeToReachPosition * Time.deltaTime);
        VCamRotation = Vector3.Lerp(VCamRotation, VCamDesiredRotation, timeToReachPosition * Time.deltaTime);
        VCamCart.m_Position = VCamPos;
        homeVCAM.transform.position = VCamCart.transform.position;
        homeVCAM.transform.rotation = Quaternion.Euler(VCamRotation);
    }

    public void MoveCameraTo(int unit)
    {
        VCamDesirePosition = unit;
        VCamDesiredRotation = rotations[unit];
    }
}
