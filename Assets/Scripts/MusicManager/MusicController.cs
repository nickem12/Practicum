using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;                                                //Get our clips
    public AudioClip clip3;
    public AudioClip clip4;

    private int oldSong = 29;

    // Use this for initialization
    void Start () {
        audioSource = this.GetComponent<AudioSource>();                     //get our audio source
    }

    public void PlayAudioClip(int index)
    {
        audioSource = this.GetComponent<AudioSource>();
        switch (index)                                                      //switch on which clip we want to play
        {
            case 0:
                audioSource.clip = clip1;
                audioSource.volume = 0.2f;
                break;
            case 1:
                audioSource.clip = clip2;
                audioSource.volume = 0.2f;
                break;
            case 2:
                audioSource.clip = clip3;
                audioSource.volume = 0.2f;
                break;
            case 3:
                audioSource.clip = clip4;
                audioSource.volume = 0.2f;
                break;
        }
        
        audioSource.Play();                                                 //Play audio.
    }

    int RandomSong()
    {
        int randomNumber = Random.Range(0, 4);
        if (randomNumber == oldSong)
        {
            int headsOrTales = Random.Range(0, 2);
            switch(headsOrTales)
            {
                case 0:
                    randomNumber++;
                    if(randomNumber > 3)
                    {
                        randomNumber = 0;
                    }
                    break;
                case 1:
                    randomNumber--;
                    if(randomNumber < 0)
                    {
                        randomNumber = 3;
                    }
                    break;
            }
        }
        oldSong = randomNumber;
        return randomNumber;
    }

    // Update is called once per frame
    void Update () {
		if(!audioSource.isPlaying)
        {
            PlayAudioClip(RandomSong());
        }
	}

}
