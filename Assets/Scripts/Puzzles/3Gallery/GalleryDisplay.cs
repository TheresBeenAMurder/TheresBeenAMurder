using UnityEngine;
using System.Collections;

public class GalleryDisplay : MonoBehaviour
{
    public GameObject[] images;

    public GameObject fakeBack;

    public AudioSource paintingAudio;

    public AudioClip powerOn;
    public AudioClip humLoop;

    public PlayerPiano piano;
    
    public void ActivateImages()
    {
        // Start the timer for the gallery puzzle hint
        StartCoroutine(piano.Hint());
        StartCoroutine(playStartupSounds());
    }

    IEnumerator playStartupSounds()
    {
        paintingAudio.clip = powerOn;
        paintingAudio.Play();
        yield return new WaitForSeconds(powerOn.length);

        foreach (GameObject image in images)
        {
            image.SetActive(true);
        }
        fakeBack.SetActive(false);

        paintingAudio.loop = true;
        paintingAudio.clip = humLoop;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateImages();
        }
    }
}
