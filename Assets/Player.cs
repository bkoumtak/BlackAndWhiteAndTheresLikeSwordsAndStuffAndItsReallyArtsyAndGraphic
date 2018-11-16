using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float walkSpeed = 10.0f;
    public float velocity;
    public float gscale; 
    bool facingRight = true;
    SpriteRenderer spriterenderer;
    Animator animator;
    Rigidbody2D rb; 

    // Use this for initialization
    void Start () {
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {
        Walk();
        velocity = rb.velocity.y;
        gscale = rb.gravityScale; 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += new Vector2(0, 9.0f);
            animator.SetBool("isJumping", true);
        }

        if (rb.velocity.y <= 0)
        {
            animator.SetBool("isJumping", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 6;
            animator.SetBool("isFalling", true); 
        }
	}

    void Walk()
    {
        float walkTranslation = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;

        transform.Translate(walkTranslation, 0, 0);
        if (walkTranslation < 0 && facingRight)
        {
            Flip();
        }
        else if (walkTranslation > 0 && !facingRight)
        {
            Flip();
        }

        if (walkTranslation == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriterenderer.flipX = !spriterenderer.flipX; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.gravityScale = 2;
        animator.SetBool("isFalling", false); 
        animator.SetBool("onGround", true); 
    }
}
