using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundManager : MonoBehaviour {

    public AudioSource audioSource;

    public AudioClip growling;
    public AudioClip death;                                                //Get our clips
    public AudioClip hurt;
    // Use this for initialization

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();                     //get our audio source
    }

    public void PlayGrowl()
    {
        audioSource.clip = growling;
        audioSource.volume = 0.3f;
        audioSource.loop = false;
        audioSource.pitch = Random.Range(0.0f, 1.0f);
        audioSource.Play();
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
    public void HurtSound()
    {
        audioSource.Stop();
        audioSource.clip = hurt;
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.pitch = 1.0f;
        audioSource.Play();
    }
}
