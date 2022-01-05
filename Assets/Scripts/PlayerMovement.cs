using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController playerController;
    private bool _isGrounded;

    [SerializeField] Transform groundCheck;

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
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))){
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        float xVelocity = Input.GetAxisRaw("Horizontal") * playerController.speed;
        float yVelocity = rb.velocity.y;
        //jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            yVelocity = playerController.jumpForce;
        }
        //Fall
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0){
            yVelocity -= playerController.jumpForce*0.3f;
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
