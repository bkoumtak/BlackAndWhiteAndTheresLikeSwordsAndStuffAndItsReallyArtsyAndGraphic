using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float velocity;
    public float maxVelocity = -40.0f;
    public Collider2D edgeCollider; 
    public float gscale;
    public Transform jumpCloud;
    public Transform[] slashes;
    public Transform eyes; 
    public bool onGround = false;
    bool isAttacking = false; 
    bool facingRight = true;
    public bool isWhite = true;
    public int health = 10;

    public Text txtHealth;
    public Text txtSouls;

    int switches = 10; 

    SpriteRenderer spriterenderer;
    SpriteRenderer eye_renderer; 
    Animator animator;
    Animator eye_animator;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        eye_renderer = eyes.GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        eye_animator = eyes.GetComponent<Animator>(); 
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.7f); 

        if (hit.collider)
        {
            if (hit.collider.tag == "Ground")
            {
                animator.SetBool("onWall", true); 
            }
                //Debug.Log("You hit the wall");
        }
    }
    // Update is called once per frame
    void Update()
    {
        txtHealth.text = "Health: " + health;
        txtSouls.text = "Souls: " + switches; 
        Walk();
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, maxVelocity));
        velocity = rb.velocity.y;
        gscale = rb.gravityScale;
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false; 
            rb.velocity += new Vector2(0, 15.0f);
            animator.SetBool("isJumping", true);
            eye_animator.SetBool("isJumping", true);
            eye_animator.SetBool("isRunning", false); 
            changeObjectColor(jumpCloud); 
            Instantiate(jumpCloud, transform.position, Quaternion.identity);
        }

        if (rb.velocity.y <= 0)
        {
            animator.SetBool("isJumping", false);
            eye_animator.SetBool("isJumping", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 10;
            animator.SetBool("isFalling", true);
            eye_animator.SetBool("isFalling", true); 
        }

        /* Determines the form of the player */
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (switches > 0) {
                isWhite = !isWhite;
                switches--; 
            }
            
        }

        changeObjectColor(transform);

        Attack();
    }

    void Walk()
    {
        float walkTranslation = 0.0f;

        if (isAttacking && (rb.velocity.y == 0))
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
            eye_animator.SetBool("isRunning", false); 
        }
        else
        {
            if (onGround)
            {
                animator.SetBool("isWalking", true);
                eye_animator.SetBool("isRunning", true);
            }        
        }
    }

    public void setAttackFalse()
    {
        animator.SetBool("AirAttack", false);
        eye_animator.SetBool("AirAttack", false); 
    }

    public bool isPlayerWhite()
    {
        return isWhite; 
    }

    public void addSwitches()
    {
        switches++; 
    }

    void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Idle-Sword"))
        {
            animator.ResetTrigger("Attack2");
            animator.ResetTrigger("Attack1");
            animator.ResetTrigger("Attack3");

            eye_animator.ResetTrigger("Attack2");
            eye_animator.ResetTrigger("Attack1");
            eye_animator.ResetTrigger("Attack3");
            isAttacking = false; 
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Jump") || animator.GetCurrentAnimatorStateInfo(0).IsName("White-Jump-Down"))
            {
                animator.SetBool("AirAttack", true);
                eye_animator.SetBool("AirAttack", true);
                //animator.ResetTrigger("AttackA");
            }

            if (onGround)
            {
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

                //if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Idle-Sword") || animator.GetCurrentAnimatorStateInfo(0).IsName("White-Run"))
                //{
                //    animator.SetTrigger("Attack1");
                //    eye_animator.SetTrigger("Attack1");
                //}
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Attack-1"))
                {
                    animator.SetTrigger("Attack2");
                    eye_animator.SetTrigger("Attack2");
                    changeObjectColor(slashes[1]);
                    Object.Instantiate(slashes[1], transform.position, Quaternion.Euler(0, rotation, 0));
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("White-Attack-2"))
                {
                    animator.SetTrigger("Attack3");
                    eye_animator.SetTrigger("Attack3");
                    changeObjectColor(slashes[2]);
                    Object.Instantiate(slashes[2], transform.position, Quaternion.Euler(0, rotation, 0));
                }
                else
                {
                    animator.SetTrigger("Attack1");
                    eye_animator.SetTrigger("Attack1");
                }
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
                t_sprite.color = new Color32(32, 23, 76, 255);
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
        eye_renderer.flipX = !eye_renderer.flipX; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("AirAttack", false);
            eye_animator.SetBool("AirAttack", false);
            rb.gravityScale = 2;
            animator.SetBool("isFalling", false);
            eye_animator.SetBool("isFalling", false);
            animator.SetBool("onGround", true);
            eye_animator.SetBool("onGround", true);
        }
        else if (collision.collider.tag == "Enemy")
        {
            Vector2 force; 

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                force = new Vector2(-8, 7);
            }
            else
            {
                force = new Vector2(8, 7);
            }

            rb.AddForce(force, ForceMode2D.Impulse);
            health--;
            IEnumerator coroutine = Hurt(0.1f, 10);
            StartCoroutine(coroutine);
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             
            }
        }
       
        Collider2D collider = collision.collider; 
        
        /*
        if (collision.collider.tag == "Ground")
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;

            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;

            if(left)
            {
                Debug.Log("Hit wall"); 
            }
        }*/
    }


    IEnumerator Hurt(float time, int repeat)
    {
        int count = 0;
        while (count <= repeat)
        {
            yield return new WaitForSeconds(time);
            spriterenderer.enabled = !spriterenderer.enabled;
            count++;
        }
        spriterenderer.enabled = true; 
    }


}
