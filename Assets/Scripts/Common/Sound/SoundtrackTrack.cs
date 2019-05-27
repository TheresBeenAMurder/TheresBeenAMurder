using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackTrack : MonoBehaviour {

    public float fadeTime = 1f;

    public SoundtrackManager parent;

    public SoundtrackLayer[] layers; //0 = drums, 1 = bass, 2 = piano, 3 = synth, 4 = brass
   
	// Use this for initialization
	void Start () {
		
	}
	
	public void startTrack()
    {
        foreach(SoundtrackLayer layer in layers)
        {
            layer.audioSource.Play();
        }
    }
  
}
