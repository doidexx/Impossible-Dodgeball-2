using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Transform target = null;
    public Vector3 offset = Vector3.zero;

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
