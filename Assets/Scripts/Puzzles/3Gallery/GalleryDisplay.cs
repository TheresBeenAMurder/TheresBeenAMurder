using UnityEngine;
using System.Collections;

public class GalleryDisplay : MonoBehaviour
{
    public GameObject[] images;
    public NPC madeline;
    public NPC victor;
    public AudioSource victorAudio;
    public AudioClip victorExclamation;

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
        

        // Play Victor's voiceline
        victorAudio.clip = victorExclamation;
        victorAudio.Play();

        // Unlock the conversation with Victor
        victor.AddAvailableConversation(64);

        // Unlock the conversation with Madeline
        madeline.AddAvailableConversation(68);

        // Start the timer for the gallery puzzle hint
        StartCoroutine(piano.Hint());
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
