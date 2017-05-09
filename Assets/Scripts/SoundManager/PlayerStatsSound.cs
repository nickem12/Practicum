using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSound : MonoBehaviour {

    public AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;                                                //Get our clips
    public AudioClip clip3;
    // Use this for initialization
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();                     //get our audio source
    }

    public void PlayAudioClip(int index)
    {
        audioSource = this.GetComponent<AudioSource>();
        switch (index)                                                      //switch on which clip we want to play
        {   
            case 0:
                audioSource.clip = clip1;
                break;
            case 1:
                audioSource.clip = clip2;
                break;
            case 2:
                audioSource.clip = clip3;
                break;
        }
        audioSource.Play();                                                 //Play audio.
    }
}
