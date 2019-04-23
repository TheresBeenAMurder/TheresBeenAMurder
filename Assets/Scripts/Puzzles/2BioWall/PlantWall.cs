using System.Collections;
using UnityEngine;

public class PlantWall : MonoBehaviour
{
    public GalleryDisplay gallery;
    private PlantPot[] plants;
    public AudioSource win;
    bool isSolved = false;

    // Hint related
    public NPC madeline;
    public AudioSource madelineAudio;
    public AudioClip madelineHint;
    public AudioSource playerAudio;
    public PlayerConversation playerConversation;

    private int? madelineCurrentPrompt;

    public void CheckForSolution()
    {
        // can't solve the puzzle more than once
        if (!isSolved)
        {
            foreach (PlantPot plant in plants)
            {
                if (!plant.IsCorrect())
                {
                    return;
                }
            }

            win.Play();
            gallery.ActivateImages();
            isSolved = true;
            madeline.RemoveAvailableConversation(60);
        }
    }

    public IEnumerator Hint()
    {
        // Wait for 3 min after the door opens to play hint
        yield return new WaitForSeconds(180);

        if (!isSolved)
        {
            while (playerConversation.inConversation || playerAudio.isPlaying)
            {
                // prevents updating madeline's voiceline while the player
                // is actively in a conversation with her
                yield return new WaitForSeconds(10);
            }

            // Play Madeline's voiceline
            madelineAudio.clip = madelineHint;
            madelineAudio.Play();
        }
    }

    public void Start()
    {
        plants = GetComponentsInChildren<PlantPot>();
    }
}
