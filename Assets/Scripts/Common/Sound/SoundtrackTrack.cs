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
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Updates a specified layer. Call when relationship value with character is changed.
    /// </summary>
    /// <param name="layerToUpdate">0 = drums, 1 = bass, 2 = piano, 3 = synth, 4 = brass</param>
    public void updateLayer(int layerToUpdate, int value)
    {
        layers[layerToUpdate].SwitchTrack(value);
    }

    public void startTrack()
    {

        foreach(SoundtrackLayer layer in layers)
        {
            layer.fadeTime = fadeTime;
        }

        layers[0].startLayer(0);
        layers[1].startLayer(0);
        layers[2].startLayer(parent.mavisRelationship);
        layers[3].startLayer(parent.victorRelationship);
        layers[4].startLayer(parent.madelineRelationship);
    }
    public void Fade()
    {
        foreach (SoundtrackLayer layer in layers)
        {
            layer.fadeOut();
        }
    }
}
