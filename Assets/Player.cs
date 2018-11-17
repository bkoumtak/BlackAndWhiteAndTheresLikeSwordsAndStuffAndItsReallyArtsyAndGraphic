using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float walkSpeed = 10.0f;
    bool facingRight = true;
    SpriteRenderer spriterenderer;
    Animator animator; 
    // Use this for initialization
    void Start () {
        spriterenderer = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        float walkTranslation = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;

        transform.Translate(walkTranslation, 0, 0); 
        if(walkTranslation < 0 && facingRight)
        {
            Flip();
            animator.SetBool("isWalking", true); 
        }
        else if (walkTranslation > 0 && !facingRight)
        {
            Flip();
            animator.SetBool("isWalking", true);
        }
        else if(walkTranslation == 0)
        {
            animator.SetBool("isWalking", false); 
        }
	}

    void Flip()
    {
        facingRight = !facingRight;
        spriterenderer.flipX = !spriterenderer.flipX; 
    }
}
