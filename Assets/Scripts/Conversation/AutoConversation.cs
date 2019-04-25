using System;
using System.Collections;
using UnityEngine;

public class AutoConversation : MonoBehaviour
{
    public ConversationUpdater convoUpdater;
    public Dialogue[] dialogue;
    public float pauseTime = .5f;

    private bool isFinished = false;

    public bool IsFinished()
    {
        return isFinished;
    }

    public IEnumerator PlayDialogue()
    {
        if (!isFinished)
        {
            foreach (Dialogue line in dialogue)
            {
                convoUpdater.TriggerVoiceLine(line.character, line.voiceLine);
                yield return convoUpdater.WaitForVoiceLine(line.character);
                yield return new WaitForSeconds(pauseTime);
            }

            isFinished = true;
        }
    }
}

[Serializable]
public struct Dialogue
{ 
    public ConversationUpdater.Character character;
    public AudioClip voiceLine;
}