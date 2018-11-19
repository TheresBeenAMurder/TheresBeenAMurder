using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour {

    //hnngn
    public SoundtrackLayer[] soundtrackLayers;


    //currently set to work with one track, will add functionlity to switch tracks


	// Use this for initialization
	void Start () {
		foreach(SoundtrackLayer audio in soundtrackLayers)
        {
           // audio.GetComponent<AudioSource>().volume = .6f;


        }
	}
	
    public void switchTrack(int track)
    {

        foreach(SoundtrackLayer sl in soundtrackLayers)
        {

            sl.switchTrack(track);

        }

    }

}
