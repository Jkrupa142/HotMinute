using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10); 
    public float smoothSpeed = 0.125f; 

    [Header("Collision Settings")]
    public float collisionRadius = 0.5f;
    public LayerMask collisionLayers; 

    [Header("Orbit Settings")]
    public float rotationSpeed = 100f;
    public float fixedPitch = 20f;

    [Header("Mouse Settings")]
    [Range(1f, 500f)]
    public float mouseSensitivity = 100f; 

    private float yaw = 0f;
    private Vector3 currentVelocity;

    void Start()
    {
        
        Vector3 angles = Quaternion.LookRotation(offset).eulerAngles;
        yaw = angles.y;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        HandleMouseInput();

       
        Quaternion rotation = Quaternion.Euler(fixedPitch, yaw, 0);

        
        Vector3 desiredPosition = target.position + rotation * offset;

       
        Vector3 direction = desiredPosition - target.position;
        float distance = direction.magnitude;
        direction.Normalize();

        RaycastHit hit;
        Vector3 adjustedPosition = desiredPosition;

        if (Physics.SphereCast(target.position, collisionRadius, direction, out hit, distance, collisionLayers))
        {
           
            adjustedPosition = target.position + direction * (hit.distance - collisionRadius);
        }

        transform.position = Vector3.SmoothDamp(transform.position, adjustedPosition, ref currentVelocity, smoothSpeed);

        transform.LookAt(target);
    }

    void HandleMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X");

        yaw += mouseX * rotationSpeed * mouseSensitivity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
