using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour {


    public AudioClip[] weaponFiringAudioClips;
    public AudioClip[] weaponReloadingAudioClip;
    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayFiringSoundEffect(int index)
    {
        source.clip = weaponFiringAudioClips[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = 0.3f;
        source.Play();
    }

    public void PlayReloadingSoundEffect(int index)
    {
        source.clip = weaponReloadingAudioClip[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = 0.3f;
        source.Play();
    }
}
