using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSound : MonoBehaviour {

    public AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;                                                //Get our clips
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
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
                audioSource.volume = 0.3f;
                break;
            case 1:
                audioSource.clip = clip2;
                audioSource.volume = 0.6f;
                break;
            case 2:
                audioSource.clip = clip3;
                audioSource.volume = 1.0f;
                break;
            case 3:
                audioSource.clip = clip4;
                audioSource.volume = 0.3f;
                break;
            case 4:
                audioSource.clip = clip5;
                audioSource.volume = 0.7f;
                break;
        }
        audioSource.loop = true;
        audioSource.pitch = Random.Range(0.0f,1.0f);
        audioSource.Play();                                                 //Play audio.
    }
    public void DeathSound()
    {
        audioSource.Stop();
        audioSource.clip = clip5;
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.pitch = 1.0f;
        audioSource.Play();
    }
}
