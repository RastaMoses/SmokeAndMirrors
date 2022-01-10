using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController playerController;
    private bool _isGrounded;

    public float fallMultiplier; //0,3f

    [SerializeField] private Transform _groundCheck;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.movementEnabled)
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
        float yVelocity = rb.velocity.y;

        //TODO -maybe- no movement direction change while jumping
        //TODO do not dead stop left/right movement when in air

        //jump
        if (playerController.jumpEnabled)
        {
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("X")) && _isGrounded)
            {
                yVelocity = playerController.jumpForce;
            }
            //Fall
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("X")) && rb.velocity.y > 0)
            {
                yVelocity -= playerController.jumpForce * fallMultiplier;
            }
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
