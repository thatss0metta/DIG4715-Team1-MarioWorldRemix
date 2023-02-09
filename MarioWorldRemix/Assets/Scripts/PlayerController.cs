using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Components")]
    public Rigidbody2D rb;
    //public Animator animator;
    public LayerMask groundLayer;  

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;

    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    
    void FixedUpdate()
    {
        moveCharacter(direction.x);
        modifyPhysics();
    }

    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);
       
        
        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
        rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
         //animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
         
    }
    void modifyPhysics()
         {
            bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

            if(Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
                    
            }    
        
         void Flip()
         {
            facingRight = !facingRight;
            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
         }
         private void OnDrawGizmos()
         {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,transform.position + Vector3.down * groundLength);
         }
}
