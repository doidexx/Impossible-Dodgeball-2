using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target = null;

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
