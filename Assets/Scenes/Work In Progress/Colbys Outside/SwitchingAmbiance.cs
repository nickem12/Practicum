using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingAmbiance : MonoBehaviour {

    public AudioSource source;
    public AudioClip[] clips;
    private GameObject theSun;

	// Use this for initialization
	void Start () {
        theSun = GameObject.FindGameObjectWithTag("Sun");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(theSun.transform.position.y <  0)
        {
            source.clip = clips[0];
            
        }
        else
        {
            source.clip = clips[1];
            
        }
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
