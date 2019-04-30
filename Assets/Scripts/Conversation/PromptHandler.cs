using System;
using System.Collections.Generic;
using UnityEngine;

public class PromptHandler : MonoBehaviour
{
    public ConversationUpdater conversationUpdater;
    public DatabaseHandler dbHandler;

    [HideInInspector]
    public bool unlockedMavisConvo = false;

    private Dictionary<int, Dictionary<int, Action>> actionHandler;

    public void DealWithPromptID(int promptId, int characterId)
    {
        if (actionHandler[characterId].ContainsKey(promptId))
        {
            actionHandler[characterId][promptId].Invoke();
        }
    }

    private void PopulateActions()
    {
        actionHandler = new Dictionary<int, Dictionary<int, Action>>(3);

        // Madline
        actionHandler[2] = new Dictionary<int, Action>();
        actionHandler[2].Add(88, this.dbHandler.FindEvidence(2, 3));
        actionHandler[2].Add(90, this.dbHandler.FindEvidence(6));

        // Victor
        actionHandler[3] = new Dictionary<int, Action>();
        actionHandler[3].Add(79, this.UpdateMadelineVictorConvo);
        actionHandler[3].Add(93, this.UnlockMavisConvo);
        actionHandler[3].Add(78, this.dbHandler.FindEvidence(1));
        actionHandler[3].Add(94, this.dbHandler.FindEvidence(7));
        actionHandler[3].Add(95, this.dbHandler.FindEvidence(8));

        // Mavis
        actionHandler[4] = new Dictionary<int, Action>();
        actionHandler[4].Add(72, this.UnlockHumanExperimentsConversation);
        actionHandler[4].Add(111, this.dbHandler.FindEvidence(9));
        actionHandler[4].Add(112, this.dbHandler.FindEvidence(10));
        actionHandler[4].Add(114, this.dbHandler.FindEvidence(13));
        actionHandler[4].Add(115, this.dbHandler.FindEvidence(12));
    }

    public void Start()
    {
        PopulateActions();
    }

    private void UnlockHumanExperimentsConversation()
    {
        string update = "UPDATE Responses SET NextPromptID = 92 WHERE ID == 89";
        dbHandler.OpenUpdateClose(update);

        update = "UPDATE Prompts SET Response2ID = 96 WHERE ID == 97";
        dbHandler.OpenUpdateClose(update);

        conversationUpdater.CloseConversation(8, true);
        conversationUpdater.CloseConversation(9, true);
        conversationUpdater.OpenConversation(8);
    }

    public void UnlockMavisConvo()
    {
        unlockedMavisConvo = true;

        conversationUpdater.OpenConversation(11);
    }

    // Unlocks the conversation with Madeline about Victor.
    private void UpdateMadelineVictorConvo()
    {
        this.dbHandler.FindEvidence(5);

        string update = "UPDATE Prompts SET Response1ID = 87 WHERE ID == 88";
        dbHandler.OpenUpdateClose(update);

        if (!conversationUpdater.madeline.CanTalk())
        {
            conversationUpdater.madeline.AddAvailableConversation(88);
        }
    }
}
