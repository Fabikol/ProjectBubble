using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float dashForce = 20;
    
    private Vector2 moveInputValue;
    private bool attackPressed=false;

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

    private void OnAttack(InputValue value)
    {
        Debug.Log("Attack");
        attackPressed = true;
    }

    private void MoveLogic()
    {
        
        Vector2 result = moveInputValue * acceleration * Time.fixedDeltaTime;
        if (result != Vector2.zero)
        {
            float speed = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude + result.magnitude;
            float fallSpeed = rb.linearVelocity.y;
            rb.linearVelocity = new Vector3(result.x, 0, result.y).normalized * speed + new Vector3(0, fallSpeed, 0);
            
            if (attackPressed)
            {
                attackPressed = false;
                Vector3 dashDirection = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(dashDirection.normalized * dashForce, ForceMode.VelocityChange);
            }
        }
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }
}
