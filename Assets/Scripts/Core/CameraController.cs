using UnityEngine;

namespace ID.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player = null;
        [SerializeField] private float smooth = 0.125f;
        [SerializeField] private float verticalOffset = 0;
        [SerializeField] private float lookAtOffset = 0;

        private void Start()
        {
            if (!player)
                player = GameObject.Find("Player").transform;
        }

        private void LateUpdate()
        {
            if (!player) return;
            var position = transform.position;
            var position1 = player.position;
            var smoothX = Mathf.Lerp(position.x, position1.x, smooth * Time.deltaTime);
            var smoothY = Mathf.Lerp(position.y, position1.y + verticalOffset, smooth * Time.deltaTime);
            position = new Vector3(smoothX, smoothY, position.z);
            transform.position = position;
            var lookAtPosition = new Vector3(position1.x, position1.y + lookAtOffset, position1.z);
            transform.LookAt(lookAtPosition);
        }
    }
}
