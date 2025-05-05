using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; 
    public float rotationSpeed = 100f;

    void Update()
    {
        Vector3 rotationAmount = rotationAxis.normalized * rotationSpeed * Time.deltaTime;

        transform.Rotate(rotationAmount, Space.World);
    }
}
