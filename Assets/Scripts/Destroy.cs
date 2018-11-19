using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public float seconds = 0.2f; 
	// Use this for initialization
	void Start () {
        Destroy(gameObject, seconds);
    }
}
