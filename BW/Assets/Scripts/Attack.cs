using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private Animator attack_slash_animation;
    public bool slash = false;
    private int slash_count = 0;
    private int slash_counter;
    private int slash_pos;

	// Use this for initialization
	void Start () {
        attack_slash_animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            slash = true;
        }

        if (slash)
        {
            slash = false;
            attack_slash_animation.SetBool("slash_check", true);
            slash_count = 20;
            slash_counter = 1;
        }

        if (attack_slash_animation.GetNextAnimatorStateInfo(0).IsName("Attack3"))
        {
            attack_slash_animation.SetBool("slash_check", false);
        }

        if (attack_slash_animation.GetNextAnimatorStateInfo(0).IsName("Idle"))
        {
            attack_slash_animation.SetBool("slash_check", false);
        }

        slash_count -= slash_counter;

        if (slash_count <= 0)
        {
            attack_slash_animation.SetBool("slash_check", false);
            slash_counter = 0;
        }

	}
}
