using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackLayer : MonoBehaviour {


    public AudioClip[] tracks;
    public AudioSource audioSource;
    public int defaultTrack;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[defaultTrack];
        audioSource.volume = 0;
        audioSource.Play();
	}
	
	public void switchTrack(int track)
    {

        audioSource.clip = tracks[track];
    }

    public void increaseLayer()
    {

        if(audioSource.volume < 1)
        {

            audioSource.volume += 0.2f;

        }

    }

    public void decreaseLayer()
    {

        if (audioSource.volume > .2)
        {

            audioSource.volume -= 0.2f;

        }

    }

    
}
