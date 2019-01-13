using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioSource player;

    public AudioClip clickSound;

	// Use this for initialization
	public void Click()
    {
        player.clip = clickSound;
        player.Play();
    }
}
