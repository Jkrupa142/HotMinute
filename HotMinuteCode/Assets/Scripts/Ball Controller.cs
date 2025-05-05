using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotationInput = 1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotationInput = -1f;
        }

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraForward * moveVertical + cameraRight * moveHorizontal;

        rb.AddForce(movement * speed);

        if (rotationInput != 0f)
        {
            Vector3 torque = Vector3.up * rotationInput * rotationSpeed * Time.fixedDeltaTime;
            rb.AddTorque(torque, ForceMode.Force);
        }
    }
}
