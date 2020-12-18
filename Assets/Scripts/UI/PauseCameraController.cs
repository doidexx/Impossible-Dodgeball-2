using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCameraController : MonoBehaviour
{
    public Transform target = null;
    public Vector3 offset = Vector3.zero;
    public Vector3 lookAtOffset = Vector3.zero;

    private void OnEnable()
    {
        target = FindObjectOfType<CameraTarget>().transform;
        transform.position = target.position + offset;
        transform.LookAt(target.position + lookAtOffset);
    }
}
