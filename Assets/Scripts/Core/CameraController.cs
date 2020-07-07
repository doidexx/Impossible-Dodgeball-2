using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] float smooth = 0.125f;
    [SerializeField] float verticalOffset = 0;
    [SerializeField] float lookAtOffset = 0;

    private void LateUpdate()
    {
        if (!player)
        {
            Debug.LogError("Please Assign a Player Transform");
            return;
        }
        float smoothX = Mathf.Lerp(transform.position.x, player.position.x, smooth * Time.deltaTime);
        float smoothY = Mathf.Lerp(transform.position.y, player.position.y + verticalOffset, smooth * Time.deltaTime);
        transform.position = new Vector3(smoothX, smoothY, transform.position.z);
        var lookAtPosition = new Vector3(player.position.x, player.position.y + lookAtOffset, player.position.z);
        transform.LookAt(lookAtPosition);
    }
}
