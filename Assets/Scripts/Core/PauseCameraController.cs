using UnityEngine;

public class PauseCameraController : MonoBehaviour
{
    public Transform target = null;
    public Vector3 offset = Vector3.zero;
    public Vector3 lookAtOffset = Vector3.zero;

    private void OnEnable()
    {
        transform.position = target.position + offset;
        transform.LookAt(target.position + lookAtOffset);
    }
}
