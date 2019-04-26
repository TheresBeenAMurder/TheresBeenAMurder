using System.Collections;
using UnityEngine;

public class PlantWall : MonoBehaviour
{
    public ConversationUpdater conversationUpdater;
    public GalleryDisplay gallery;
    private PlantPot[] plants;
    public AudioClip victorReact;
    public AudioSource win;
    bool isSolved = false;

    // Hint related
    public AudioClip madelineHint;
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
            conversationUpdater.CloseConversation(1, true);
            conversationUpdater.TriggerVoiceLine(ConversationUpdater.Character.Victor, victorReact);
            conversationUpdater.OpenConversation(2);
            conversationUpdater.OpenConversation(4);
        }
    }

    public IEnumerator Hint()
    {
        // Wait for 3 min after the door opens to play hint
        yield return new WaitForSeconds(180);

        if (!isSolved)
        {
            yield return playerConversation.WaitToTalk();
            conversationUpdater.TriggerVoiceLine(ConversationUpdater.Character.Madeline, madelineHint);
        }
    }

    public void Start()
    {
        plants = GetComponentsInChildren<PlantPot>();
    }
}
