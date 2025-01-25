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
        rb.linearVelocity += new Vector3(result.x, 0, result.y);
        if (((rb.linearVelocity + new Vector3(result.x, 0, result.y)).magnitude > maxSpeed))
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }
}
