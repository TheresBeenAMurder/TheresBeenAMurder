using UnityEngine;

public class GalleryDisplay : MonoBehaviour
{
    public GameObject[] images;
    public NPC victor;
    public AudioSource victorAudio;
    public AudioClip victorExclamation;

    public GameObject fakeBack;

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
        victor.UpdateNextPrompt(20);

        // Start the timer for the gallery puzzle hint
       // StartCoroutine(galleryPuzzle.Hint());
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
