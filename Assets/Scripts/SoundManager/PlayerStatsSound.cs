using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSound : MonoBehaviour {

    public AudioSource audioSource;

    public AudioClip heartBeat1;
    public AudioClip heartBeat2;                                                //Get our clips
    public AudioClip heartBeat3;
    public AudioClip breathing1;
    public AudioClip breathing2;
    public AudioClip death;
    public AudioClip stomachGrowling1;
    public AudioClip stomachGrowling2;
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
                audioSource.clip = heartBeat1;
                audioSource.volume = 0.3f;
                break;
            case 1:
                audioSource.clip = heartBeat2;
                audioSource.volume = 0.6f;
                break;
            case 2:
                audioSource.clip = heartBeat3;
                audioSource.volume = 1.0f;
                break;
            case 3:
                audioSource.clip = breathing1;
                audioSource.volume = 0.3f;
                break;
            case 4:
                audioSource.clip = breathing2;
                audioSource.volume = 0.7f;
                break;
            case 5:
                audioSource.clip = stomachGrowling1;
                audioSource.volume = 0.5f;
                break;
            case 6:
                audioSource.clip = stomachGrowling2;
                audioSource.volume = 0.9f;
                break;
        }
        audioSource.loop = true;
        audioSource.pitch = Random.Range(0.0f,1.0f);
        audioSource.Play();                                                 //Play audio.
    }
    public void DeathSound()
    {
        audioSource.Stop();
        audioSource.clip = death;
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.pitch = 1.0f;
        audioSource.Play();
    }
}
