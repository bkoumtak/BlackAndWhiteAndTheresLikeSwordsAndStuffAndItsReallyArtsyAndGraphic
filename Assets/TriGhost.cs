using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriGhost : MonoBehaviour {
    Player player;
    bool detectedPlayer = false;
    public bool isBlack = false; 
    float speed = 10.0f;
    Animator animator; 
   

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        speed  = Random.Range(5.0f, 8.0f);
        animator = GetComponent<Animator>(); 
	}
	
	// Update is called once per frame
	void Update () {

		if (detectedPlayer)
        {
            float step = speed * Time.deltaTime;
            Vector3 dest = player.gameObject.transform.position; 

            if (transform.position.x < dest.x)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y); 
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            
            transform.position = Vector2.MoveTowards(transform.position, dest, step);
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine("Switch"); 
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
    
    void SwitchColor()
    {
        if (!isBlack)
        {
            isBlack = true;
            animator.SetBool("isBlack", true); 
        }
        else
        {
            isBlack = false;
            animator.SetBool("isBlack", false); 
        }
    }

    IEnumerator Switch()
    {
        while (true)
        {
            float seconds = Random.Range(2.0f, 4.0f);
            SwitchColor();
            yield return new WaitForSeconds(seconds);
        }
    }

}
