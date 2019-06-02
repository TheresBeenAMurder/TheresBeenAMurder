using UnityEngine;

public class SoundtrackLayer : MonoBehaviour
{
    public AudioSource audioSource;
    public int currentTrack;
    public AudioClip[] tracks;

    bool fadingIn = false;
    bool fadingOut = false;

    public float fadeTime;

    public float maxFade = .5f;


	
	
    
}
