using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("Running_1");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetTrigger("EndRun");
        }
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attacking");
        }
    }
}
