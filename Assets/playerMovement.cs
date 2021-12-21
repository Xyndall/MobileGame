 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    public float acceleration = 2;



    public float jumpForce;
    private float moveInput;
    public int extraJumps;
    public int extraJumpsValue;

    private Rigidbody2D rb;

    private bool facingRight = true;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask WhatIsGound;




    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGound);

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * (maxSpeed * acceleration), rb.velocity.y);

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        } else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Update()
    {

        if(isGrounded == true)
        {
            extraJumps = 1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

    }

}
