using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player = null;
    public Transform ragdoll = null;
    public Vector3 offset = Vector3.zero;
    public float followSpeed = 5;

    private void LateUpdate()
    {
        if (player.GetComponent<Player>().playerState == PlayerState.Playing)
        {
            var playerOffset = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, playerOffset, followSpeed * Time.deltaTime);
        }
        transform.LookAt(ragdoll.position);
    }
}
