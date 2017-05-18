using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomClick : MonoBehaviour {

    public AudioSource Source;

    //public AudioClip clip1;
    //public AudioClip clip2;
    //public AudioClip clip3;

    public AudioClip[] clipArray;

    private int RandomNumber = 29;
    private int counter = 0;
    private float clickTimer = 1f;

	// Use this for initialization
	void Start () {  
       
            Source.clip = clipArray[Random.Range(0, clipArray.Length)];
            Source.Play();
        
	}
	
	// Update is called once per frame
	void Update () {

        clickTimer -= Time.deltaTime;

        if (clickTimer <= 0)
        {
            Source.clip = clipArray[Random.Range(0, clipArray.Length)];

            Source.Play();

            clickTimer = 1f;
        }
       

    }
}
