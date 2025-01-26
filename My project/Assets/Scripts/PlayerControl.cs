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

    public GameObject Respawnpoint;

    
    public Transform cameraTransform;

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

    private void MoveLogic()
    {
        
        Vector3 result = new Vector3(moveInputValue.x, 0f, moveInputValue.y) * acceleration * Time.fixedDeltaTime;
        
        
        
        if (result != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(result.x, result.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
            float speed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude + result.magnitude;
            float fallSpeed = rb.linearVelocity.y;
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            rb.linearVelocity = moveDir * speed + new Vector3(0, fallSpeed, 0);
            
            if (attackPressed)
            {
                attackPressed = false;
                Vector3 dashDirection = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
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

    }
    
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("world"))
        {
            //Debug.Log("Boden");
            isOnGround = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("world"))
        {
            //Debug.Log("Luft");
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

}

