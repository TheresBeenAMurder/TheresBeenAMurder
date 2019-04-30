using System.Collections;
using UnityEngine;

// Class necessary so player can only be in conversation with one
// NPC at the same time, regardless of if they are within the triggers
// of multiple NPCs.
public class PlayerConversation : MonoBehaviour
{
    public ConversationUpdater conversationUpdater;
    public bool inConversation = false;
    public AudioSource playerAudio;

    // Re-adds the accusation conversation for each character
    // Highly inefficient, probably needs to be fixed later.
    public void AddAccusationConversations()
    {
        conversationUpdater.CloseConversation(12, true);
        conversationUpdater.CloseConversation(13, true);
        conversationUpdater.CloseConversation(14, true);

        conversationUpdater.OpenConversation(12);
        conversationUpdater.OpenConversation(13);
        conversationUpdater.OpenConversation(14);
    }

    public IEnumerator WaitToTalk()
    {
        while (inConversation || playerAudio.isPlaying)
        {
            yield return new WaitForSeconds(10);
        }
    }
}
