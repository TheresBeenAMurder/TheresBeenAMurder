using UnityEngine;

public class GalleryDisplay : MonoBehaviour
{
    public GameObject[] images;
    public CenterFrameButton galleryPuzzle;
    public NPC victor;
    public AudioSource victorAudio;
    public AudioClip victorExclamation;

    public void ActivateImages()
    {
        foreach(GameObject image in images)
        {
            image.SetActive(true);
        }

        // Play Victor's voiceline
        victorAudio.clip = victorExclamation;
        victorAudio.Play();

        // Unlock the conversation with Victor
        victor.UpdateNextPrompt(20);

        // Start the timer for the gallery puzzle hint
        StartCoroutine(galleryPuzzle.Hint());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateImages();
        }
    }
}
