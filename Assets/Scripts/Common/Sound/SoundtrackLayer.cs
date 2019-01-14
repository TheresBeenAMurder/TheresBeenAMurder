using UnityEngine;

public class SoundtrackLayer : MonoBehaviour
{
    public AudioSource audioSource;
    public int currentTrack;
    public int defaultTrack;
    public AudioClip[] tracks;

    public void decreaseLayer()
    {
        if (audioSource.volume > .2)
        {
            audioSource.volume -= 0.2f;
        }
    }

    public void increaseLayer()
    {
        if (audioSource.volume < 1)
        {
            audioSource.volume += 0.2f;
        }
    }

    public void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[defaultTrack];
        audioSource.Play();
        currentTrack = defaultTrack;
	}
	
	public void SwitchTrack(int track)
    {
        audioSource.clip = tracks[track];
        currentTrack = track;

        audioSource.time = audioSource.time;
    }
}
