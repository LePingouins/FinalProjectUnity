using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField]private float jumpSpeed = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidBody2D;
    private float wallJumpCooldown;
    private float horizontalInput;
    private void Awake() //Called when the script is being loaded
    {
        //Grab reference to the Rigidbody2D component
        rigidBody2D = GetComponent<Rigidbody2D>(); //Get the Rigidbody2D component or to access it
        //Grab reference to the Animator component
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rigidBody2D.linearVelocity = new Vector2(horizontalInput * speed, rigidBody2D.linearVelocity.y);

        //Flips the player sprite based on the direction it is moving
        //Flips right side if moving right and left side if moving left 0.01f is used to prevent floating point errors. and 0.01f is when pressing right and -0.01f is when pressing left
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1); //Can use just vector3.one
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        
        //Set the speed parameter in the animator to the absolute value of the horizontal input
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());


        //Wall Jump Logic
        if(wallJumpCooldown > 0.2f)
        {
            

            rigidBody2D.linearVelocity = new Vector2(horizontalInput * jumpSpeed, rigidBody2D.linearVelocity.y);
            
            if (onWall() && !isGrounded())
            {
                rigidBody2D.gravityScale = 0;
                //rigidBody2D.linearVelocity = new Vector2(0, 0); same thing as bellow
                rigidBody2D.linearVelocity = Vector2.zero;
            }
            else
                rigidBody2D.gravityScale = 1;

            //Jumping
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //if (Input.GetKey(KeyCode.F))
        //{
        //    shield();
        //}
    }

    // Reset to not rotate character
    //private void FixedUpdate()
    //{
    //    rigidBody2D.rotation = 0f;
    //}
    private void Jump()
    {
        if (isGrounded())
        {
            rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, jumpSpeed);
            animator.SetTrigger("jump");
            AudioManager.Instance.PlaySFX("Jump");

        }
        else if (onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            //rigidBody2D.linearVelocity = new Vector2(-transform.localScale.x * jumpSpeed, jumpSpeed); //same thing as bellow
            rigidBody2D.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6); //same thing as above
            if (horizontalInput == 0)
            {
                rigidBody2D.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);  
            }
            else
            {
                rigidBody2D.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                wallJumpCooldown = 0;
            }
        }
    }

    //private void shield()
    //{
    //    animator.SetTrigger("shield");
    //}

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.size, 0, Vector2.down, 0.1f, groundLayer);       
        return raycastHit2D.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}