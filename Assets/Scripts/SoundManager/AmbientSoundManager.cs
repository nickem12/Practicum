using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour {

    public AudioClip clip;
    private AudioSource source;
    public float timer;
    private float timerIncrement;

	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
        source.clip = clip;
        timerIncrement = 0;
        source.Play();
	}
	
	// Update is called once per frame
	void Update () {

        timerIncrement += Time.deltaTime;

        if(timerIncrement >= timer)
        {
            source.Play();
            timerIncrement = 0;
        }
	}
}
