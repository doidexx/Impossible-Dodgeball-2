using UnityEngine;

namespace ID.Core
{
    public class Shredder : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
