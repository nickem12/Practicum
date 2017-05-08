using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour {

    AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;
	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
	}
	
    public void PlayAudioClip(int index)
    {
        audioSource = this.GetComponent<AudioSource>();
        switch(index)
        {
            case 0:
                audioSource.clip = clip1;
                break;
            case 1:
                audioSource.clip = clip2;
                break;
        }
        audioSource.Play();
    }
}
