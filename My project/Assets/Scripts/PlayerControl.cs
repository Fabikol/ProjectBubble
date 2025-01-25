using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float jumpForce = 50f;
    
    private Vector2 moveInputValue;

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (rb.linearVelocity.y == 0)
        {
            rb.linearVelocity += Vector3.up * jumpForce;
        }
    }

    private void MoveLogic()
    {
        
        Vector2 result = moveInputValue * acceleration * Time.fixedDeltaTime;
        if (result != Vector2.zero)
        {
            float speed = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude + result.magnitude;
            float fallSpeed = rb.linearVelocity.y;
            rb.linearVelocity = new Vector3(result.x, 0, result.y).normalized * speed + new Vector3(0, fallSpeed, 0);
        }
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }
}
