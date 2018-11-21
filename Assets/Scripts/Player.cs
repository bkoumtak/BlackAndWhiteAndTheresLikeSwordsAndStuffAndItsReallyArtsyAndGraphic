using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float velocity;
    public float maxVelocity = -40.0f; 
    public float gscale;
    public Transform jumpCloud;
    public Transform[] slashes;
    bool onGround = false;
    bool isAttacking = false; 
    bool facingRight = true;
    public bool isWhite = true; 

    SpriteRenderer spriterenderer;
    Animator animator;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, maxVelocity));
        velocity = rb.velocity.y;
        gscale = rb.gravityScale;
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false; 
            rb.velocity += new Vector2(0, 15.0f);
            animator.SetBool("isJumping", true);
            changeObjectColor(jumpCloud); 
            Instantiate(jumpCloud, transform.position, Quaternion.identity); 
        }

        if (rb.velocity.y <= 0)
        {
            animator.SetBool("isJumping", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 10;
            animator.SetBool("isFalling", true);
        }

        /* Determines the form of the player */
        if (Input.GetKeyDown(KeyCode.K))
        {
            isWhite = !isWhite;
        }

        changeObjectColor(transform);

        Attack();
    }

    void Walk()
    {
        float walkTranslation = 0.0f;
        
        if (isAttacking && onGround)
        {
            walkTranslation = 0.0f; 
        } 
        else
        {
            walkTranslation = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
        }
        
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

    void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Idle-Sword"))
        {
            animator.ResetTrigger("Attack2");
            animator.ResetTrigger("Attack1");
            animator.ResetTrigger("Attack3");
            isAttacking = false; 
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            isAttacking = true; 
            float rotation = 0; 
            if (facingRight)
            {
                rotation = 0f; 
            }
            else
            {
                rotation = 180f; 
            } 

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Idle-Sword")) { 
                animator.SetTrigger("Attack1");
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Attack-1"))
            {
                animator.SetTrigger("Attack2");
                changeObjectColor(slashes[1]);
                Object.Instantiate(slashes[1], transform.position, Quaternion.Euler(0, rotation, 0));
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Attack-2"))
            {
                animator.SetTrigger("Attack3");
                changeObjectColor(slashes[2]);
                Object.Instantiate(slashes[2], transform.position, Quaternion.Euler(0, rotation, 0));
            }
        }
    }

    void changeObjectColor(Transform t)
    {
        if (t != null)
        {
            SpriteRenderer t_sprite = t.GetComponent<SpriteRenderer>(); ;
            if (isWhite)
            {
                t_sprite.color = Color.white;
            }
            else
            {
                t_sprite.color = Color.black;
            }
        }
    }

    void Attack1()
    {
        float rotation = 0;
        if (facingRight)
        {
            rotation = 0f;
        }
        else
        {
            rotation = 180f;
        }
        changeObjectColor(slashes[0]);
        Object.Instantiate(slashes[0], transform.position, Quaternion.Euler(0, rotation, 0));
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriterenderer.flipX = !spriterenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true; 
        rb.gravityScale = 2;
        animator.SetBool("isFalling", false);
        animator.SetBool("onGround", true);
    }
}
