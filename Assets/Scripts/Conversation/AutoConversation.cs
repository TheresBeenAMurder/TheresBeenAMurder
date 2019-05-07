using System;
using System.Collections;
using UnityEngine;

public class AutoConversation : MonoBehaviour
{
    public ConversationUpdater convoUpdater;
    public Dialogue[] dialogue;
    public float pauseTime = .7f;
    public PlayerConversation playerConversation;

    private bool isFinished = false;
    private bool isStopped = false;

    public bool IsFinished()
    {
        return isFinished;
    }

    public IEnumerator PlayDialogue()
    {
        yield return StartCoroutine(playerConversation.WaitToTalk());

        if (!isFinished && !isStopped)
        {
            playerConversation.inConversation = true;

            foreach (Dialogue line in dialogue)
            {
                if (isStopped)
                {
                    break;
                }

                convoUpdater.TriggerVoiceLine(line.character, line.voiceLine);
                yield return StartCoroutine(convoUpdater.WaitForVoiceLine(line.character));
                yield return new WaitForSeconds(pauseTime);
            }

            isFinished = true;
            playerConversation.inConversation = false;
        }
    }

    public void StopDialogue()
    {
        isStopped = true;
    }
}

[Serializable]
public struct Dialogue
{ 
    public ConversationUpdater.Character character;
    public AudioClip voiceLine;
}