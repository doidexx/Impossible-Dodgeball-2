using UnityEngine;

public class CamerasTarget : MonoBehaviour
{
    public Transform target = null;

    private void Update()
    {
        transform.position = target.position;
    }
}
