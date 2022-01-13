using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    //Configure Params
    [SerializeField] private Transform _groundCheck;

    //State
    private bool _isGrounded;
    bool isMoving;
    bool isFalling;
    bool leverPulling;

    //Cached Component Reference
    Rigidbody2D rb;
    PlayerController playerController;
    Animator animator;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.movementEnabled || leverPulling)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Rails")))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        float xVelocity = Input.GetAxisRaw("Horizontal") * playerController.speed;
        if(rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        float yVelocity = rb.velocity.y;

        //TODO -maybe- no movement direction change while jumping

        //jump
        if (playerController.jumpEnabled)
        {
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("X")) && _isGrounded)
            {
                yVelocity = playerController.jumpForce;
                animator.SetTrigger("jump");
                
            }
            //Fall
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("X")) && rb.velocity.y > 0)
            {
                yVelocity -= playerController.jumpForce * 0.3f;
                
            }
        }
        
        rb.velocity = new Vector2(xVelocity, yVelocity);

        
        if (rb.velocity.y < 0.1f && rb.velocity.y > -0.1f && _isGrounded)
        {
            isFalling = false;
            
        }
        else
        {
            isFalling = true;
        }
        Animations();
    }


    private void Animations()
    {
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if(rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isFalling",isFalling);
    }


    public void SetLeverPulling()
    {
        animator.SetTrigger("lever");
        leverPulling = true;
    }

    public void StopLeverPulling()
    {
        Debug.Log("Stop Lever Pulling");
        leverPulling = false;
    }
}
