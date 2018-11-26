using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    Player player; 
	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.addSwitches();
            Destroy(gameObject); 
        }
    }
}
