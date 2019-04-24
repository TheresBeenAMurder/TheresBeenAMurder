using System;
using System.Collections;
using UnityEngine;

public class InvitationSpawner : MonoBehaviour
{
    public Door doorPuzzle;
    public GameObject invitationPrefab;
    public GameObject mavis;
    public GameObject invitationObject;

    public NPCAnimator mavisAnimator;

    public NPCAnimator madelineAnimator;
    public NPCAnimator victorAnimator;

    // Opening Dialogue
    public AudioSource DetectiveAudio;
    public AudioSource MadelineAudio;
    public AudioSource MavisAudio;
    public AudioSource VictorAudio;

    public OpeningConversationLines[] openingDialogue;

    private bool isDoneConversation = false;

    private IEnumerator OpeningDialogue()
    {
        foreach (OpeningConversationLines conversationLine in openingDialogue)
        {
            // Assigned literally only to make Unity happy
            AudioSource currentCharacter = MadelineAudio;
            switch (conversationLine.character)
            {
                case Character.Detective:
                    currentCharacter = DetectiveAudio;
                    break;
                case Character.Madeline:
                    currentCharacter = MadelineAudio;
                    break;
                case Character.Mavis:
                    currentCharacter = MavisAudio;
                    break;
                case Character.Victor:
                    currentCharacter = VictorAudio;
                    break;
            }

            currentCharacter.clip = conversationLine.voiceLine;
            currentCharacter.Play();

            yield return new WaitForSeconds(currentCharacter.clip.length + .5f);
        }
    }

    public void Start()
    {
        StartCoroutine(OpeningDialogue());
    }

    public void StopOpeningDialogue()
    {
        StopCoroutine(OpeningDialogue());
    }

    public void Update ()
    {
		if (isDoneConversation)
        {
            invitationObject.SetActive(true);
            mavisAnimator.changeState(NPCAnimator.CHARACTERSTATE.HAND);
            victorAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
            madelineAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);

            StartCoroutine(doorPuzzle.Hint());

            enabled = false;
        }
	}

    [Serializable]
    public struct OpeningConversationLines
    {
        public Character character;
        public AudioClip voiceLine;
    }

    public enum Character
    {
        Detective,
        Madeline,
        Mavis,
        Victor
    }
}
