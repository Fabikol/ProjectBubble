using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] public float maxSpeed = 100f;
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private float dashForce = 20;

    private bool isOnGround = false;
    public event EventHandler<PlayerControl> OnDeath = delegate { };

    public GameObject Respawnpoint;


    public Transform cameraTransform;

    [Header("Slope Handling")]
    public float playerHeight = 1;
    public float maxSlopeAngle = 45f;
    private RaycastHit slopeHit;

    private Vector2 moveInputValue;
    private Vector3 moveDir;
    private bool attackPressed = false;

    public float KnockbackForce;

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f, LayerMask.GetMask("world")))
        {
            Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(10, 10, 10), Color.red);
            Debug.DrawLine(transform.position, slopeHit.point, Color.red, 1000);
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            Debug.Log($"Slope Angle: {angle}");
            return angle < maxSlopeAngle;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    private void OnJump(InputValue value)
    {
        if (isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void MoveLogic()
    {

        Vector3 result = new Vector3(moveInputValue.x, 0f, moveInputValue.y);




        if (result != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(result.x, result.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * acceleration * Time.fixedDeltaTime;


            if (OnSlope() && new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude < maxSpeed)
            {
                rb.AddForce(GetSlopeMoveDirection() * acceleration, ForceMode.Acceleration);
            }

            else if (new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude < maxSpeed)
            {
                rb.AddForce(moveDir * acceleration, ForceMode.Acceleration);
            }

            if (attackPressed)
            {
                attackPressed = false;
                Vector3 dashDirection = moveDir;
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(dashDirection.normalized * dashForce, ForceMode.VelocityChange);
            }
        }
        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.9f, rb.linearVelocity.y, rb.linearVelocity.z * 0.9f);
        }
    }


    private void OnAttack(InputValue value)
    {
        //Debug.Log("Attack");
        attackPressed = true;
    }

    private void FixedUpdate()
    {
        MoveLogic();

        if (transform.position.y < -20)
        {
            OnDeath(this, this);
        }
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("world"))
        {
            //Debug.Log("Boden");
            rb.useGravity = false;
            isOnGround = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("world"))
        {
            //Debug.Log("Luft");
            rb.useGravity = true;
            isOnGround = false;
        }
    }

    public void Respawn()
    {
        if (Respawnpoint == null)
        {
            Debug.LogError("Respawnpoint is not assigned! Make sure it is set in the inspector.");
            return;
        }

        transform.position = Respawnpoint.transform.position;
        Debug.Log("Player respawned at: " + Respawnpoint.transform.position);
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * KnockbackForce, ForceMode.VelocityChange);
        }
    }

}


