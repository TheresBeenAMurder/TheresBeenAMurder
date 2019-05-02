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
        foreach(GameObject image in images)
        {
            image.SetActive(true);
        }
        fakeBack.SetActive(false);

        // Start the timer for the gallery puzzle hint
        StartCoroutine(piano.Hint());
        StartCoroutine(playStartupSounds());
    }

    IEnumerator playStartupSounds()
    {
        paintingAudio.clip = powerOn;
        paintingAudio.Play();
        yield return new WaitForSeconds(powerOn.length);
        paintingAudio.loop = true;
        paintingAudio.clip = humLoop;
    }

    private void Start()
    {
        // Disable gallery puzzle until paintings are revealed
       // centerFrame.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateImages();
        }
    }
}
