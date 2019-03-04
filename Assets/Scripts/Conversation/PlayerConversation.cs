using UnityEngine;

// Class necessary so player can only be in conversation with one
// NPC at the same time, regardless of if they are within the triggers
// of multiple NPCs.
public class PlayerConversation : MonoBehaviour
{
    public bool inConversation = false;
    public NPC[] npcs;

    public void CanAccuse()
    {
        foreach (NPC npc in npcs)
        {
            npc.canAccuse = true;
        }
    }
}
