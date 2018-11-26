using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriGhost : MonoBehaviour {
    Player player;
    bool detectedPlayer = false;
    public bool isBlack = false; 
    float speed = 10.0f;
    Animator animator;
    bool flipped = false;
    Rigidbody2D rb; 
   

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        speed  = Random.Range(5.0f, 8.0f);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {

		if (detectedPlayer)
        {
            if (player != null)
            {
                float step = speed * Time.deltaTime;
                Vector3 dest = player.gameObject.transform.position;

                if (transform.position.x < dest.x)
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
                    flipped = true;
                }
                else
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    flipped = false;
                }

                transform.position = Vector2.MoveTowards(transform.position, dest, step);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            if (flipped)
            {
                transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
            }
             
            if (player.isPlayerWhite() && !isBlack)
            {
                Destroy(transform.GetChild(0).gameObject);
                isBlack = true; 
            }

            if (!player.isPlayerWhite() && isBlack)
            {
                Destroy(gameObject);
            }
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            detectedPlayer = true;
           // Debug.Log("Player detected");
        }
      
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            detectedPlayer = false;
           // Debug.Log("Lost sight of player");
            StopCoroutine("Switch"); 
        }
    }

}
