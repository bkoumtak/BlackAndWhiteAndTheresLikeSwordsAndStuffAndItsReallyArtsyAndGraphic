using System;
using UnityEngine; 
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

public class LostSoul : MonoBehaviour {
    Rigidbody2D rb;
    Animator animator;
    Player player;
    Renderer rend; 
    float speed = 4.0f;
    float offset = 1.0f;
    bool onGround = false;
    public bool flipped = false;
    public bool right = false; 
    public bool isBlack = false;
    public bool isAware = false; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<Player>();
        rend = GetComponent<Renderer>();
        Debug.Log(rend);
	}

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        //if (isBlack)
        //    step *= 2; 
        if (player != null)
        {


            Vector3 dest = player.gameObject.transform.position;

            Collider2D col = GetComponent<Collider2D>();

            if (isAware)
            {
                if (dest.x < transform.position.x)
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    flipped = false;
                }
                else
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
                    flipped = true;
                }

                if (Mathf.Abs(dest.x - transform.position.x) > 1.5f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, dest, step);
                    animator.SetBool("isAttacking", false);
                    //transform.position = new Vector2(transform.position.x, 0); 
                }
                else
                {

                    animator.SetBool("isAttacking", true);
                    animator.ResetTrigger("Hit");

                }
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }
        }
        
    }
    // Update is called once per frame
    void Update() {
      
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Weapon")
        {
            Vector2 force; 
            if (flipped)
            {
                force = new Vector2(-7, 5);
              
            }
            else
            {
                force = new Vector2(7, 5);
            }
            
            rb.AddForce(force, ForceMode2D.Impulse);

            if(player.isPlayerWhite())
            {
                animator.SetTrigger("Hit");
                isBlack = true; 
            }
            else 
            {
                if(isBlack)
                {
                    Destroy(gameObject);
                }
               
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAware = true;
            animator.SetBool("isAware", true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAware = false;
            animator.SetBool("isAware", false);
        }
    }
}
