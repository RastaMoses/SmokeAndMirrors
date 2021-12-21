using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xVelocity = Input.GetAxisRaw("Horizontal") * pc.speed;
        float yVelocity = rb.velocity.y;
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = pc.jumpForce;
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0){
            yVelocity -= pc.jumpForce*0.3f;
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
