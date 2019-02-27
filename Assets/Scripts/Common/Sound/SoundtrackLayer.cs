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


    public void Update ()
    {
        if(fadingIn)
        {
            if (audioSource.volume + (maxFade / fadeTime * Time.deltaTime) < maxFade)
            {
                audioSource.volume += (maxFade / fadeTime * Time.deltaTime);
            }
            else //reach max volume & we're done fading in
            {
                audioSource.volume = maxFade;
                fadingIn = false;
            }
            
                
        }

        if(fadingOut)
        {
            if (audioSource.volume - (maxFade / fadeTime * Time.deltaTime) > 0)
            {
                audioSource.volume -= (maxFade / fadeTime * Time.deltaTime);
            }
            else //reach 0 volume & we're done fading out
            {
                audioSource.volume = 0;
                fadingOut = false;
            }
        }
        
	}
	
	public void SwitchTrack(int track)
    {
        float time = audioSource.time;
        audioSource.clip = tracks[track];
        currentTrack = track;

        audioSource.time = time;
    }

    public void startLayer(int trackNumber)
    {
        fadingIn = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[trackNumber];
        audioSource.Play();
        audioSource.volume = 0;
        currentTrack = trackNumber;
    }

    public void fadeOut()
    {
        fadingOut = true;
    }
}
