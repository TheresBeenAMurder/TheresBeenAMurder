using UnityEngine;

public class GalleryDisplay : MonoBehaviour
{
    public CenterFrame centerFrame;
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

        // Enable gallery puzzle
        centerFrame.enabled = true;

        // Play Victor's voiceline
        victorAudio.clip = victorExclamation;
        victorAudio.Play();

        // Unlock the conversation with Victor
        victor.UpdateNextPrompt(20);

        // Start the timer for the gallery puzzle hint
        StartCoroutine(galleryPuzzle.Hint());
    }

    private void Start()
    {
        // Disable gallery puzzle until paintings are revealed
        centerFrame.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateImages();
        }
    }
}
