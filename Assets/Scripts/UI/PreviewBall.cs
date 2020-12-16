using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBall : MonoBehaviour
{
    public float rotationSpeed = 2;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime + Vector3.right * Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime);
    }
}
