using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    //Configure Params
    [SerializeField] private Transform _groundCheck;
    [SerializeField] float jumpDelay = 0.1f;

    //State
    private bool _isGrounded;
    bool isMoving;
    bool isFalling;
    bool leverPulling;
    bool hasJumped; //Checks if player is waiting on jump delay

    float temporaryJumpForce; //used for shorter jumps during jumpdelay
    float yVelocity;
    float xVelocity;

    //Cached Component Reference
    Rigidbody2D rb;
    PlayerController playerController;
    Animator animator;
    SFX sfx;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        sfx = GetComponent<SFX>();
        
        
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

        xVelocity = Input.GetAxisRaw("Horizontal") * playerController.speed;
        if(rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (!hasJumped)
        {
            yVelocity = rb.velocity.y;
        }

        //TODO -maybe- no movement direction change while jumping

        //jump
        if (playerController.jumpEnabled)
        {
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("X")) && _isGrounded && !hasJumped)
            {
                hasJumped = true;
                animator.SetTrigger("jump");
                StartCoroutine(JumpDelay());
                
            }
            //Fall
            //Slows down jump during jump delay
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("X")) && hasJumped)
            {
                temporaryJumpForce = playerController.jumpForce * 0.3f;
            }
            //slows down jump after liftoff
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("X")) && yVelocity > 0)
            {
                yVelocity = playerController.jumpForce * 0.3f;
            }
        }
        
        rb.velocity = new Vector2(xVelocity, yVelocity);

        
        if (rb.velocity.y < 0.1f && rb.velocity.y > -0.1f && _isGrounded && !hasJumped)
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
        //SFX
        

        animator.SetTrigger("lever");
        leverPulling = true;
    }

    public void StopLeverPulling()
    {
        leverPulling = false;
    }

    IEnumerator JumpDelay()
    {
        //SFX
        sfx.Jump();

        //Needed for animation and better feeling
        yield return new WaitForSeconds(jumpDelay);
        yVelocity = playerController.jumpForce;
        yVelocity -= temporaryJumpForce;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        hasJumped = false;
        temporaryJumpForce = 0;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Plays Landing Sound if landing
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !_isGrounded)
        {
            sfx.Landing();
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            playerController.Respawn();
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            playerController.Goal();
        }
    }
}
