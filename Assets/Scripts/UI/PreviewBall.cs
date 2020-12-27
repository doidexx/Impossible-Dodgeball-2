using UnityEngine;

public class PreviewBall : MonoBehaviour
{
    public float rotationSpeed = 2;

    private void Start()
    {
        var color = GetComponent<Renderer>().material.color;
        GetComponent<TrailRenderer>().startColor = color;
        GetComponent<TrailRenderer>().endColor = color;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime + Vector3.right * Mathf.PingPong(-rotationSpeed, rotationSpeed) * Time.deltaTime);
    }
}
