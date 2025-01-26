using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CameraControl : MonoBehaviour
{
    public GameObject cam;
    public Transform target; // The GameObject the camera will orbit around
    public float distance = 5.0f; // Distance from the target
    public float rotationSpeed = 100.0f; // Speed of rotation
    public float verticalMinAngle = -30.0f; // Minimum vertical angle
    public float verticalMaxAngle = 60.0f; // Maximum vertical angle

    private float currentYaw = 0.0f; // Current horizontal angle
    private float currentPitch = 0.0f; // Current vertical angle

    private Vector2 moveInputValue;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraController: No target assigned.");
            enabled = false;
        }
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }
    
    private void OnLook(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();

        // Update yaw and pitch based on input and rotation speed
        currentYaw += moveInputValue.x * rotationSpeed * Time.deltaTime;
        currentPitch += -moveInputValue.y * rotationSpeed * Time.deltaTime;

        // Clamp the pitch to stay within the allowed vertical range
        currentPitch = Mathf.Clamp(currentPitch, verticalMinAngle, verticalMaxAngle);
    }

    private void UpdateCameraPosition()
    {
        // Calculate the new position and rotation of the camera
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 positionOffset = rotation * new Vector3(0, 0, -distance);

        cam.transform.position = target.position + positionOffset;
        cam.transform.LookAt(target);
    }
}
