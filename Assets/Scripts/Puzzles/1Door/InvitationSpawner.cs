using UnityEngine;

public class InvitationSpawner : MonoBehaviour
{
    public AutoConversation autoConversation;
    public Door doorPuzzle;
    public GameObject invitationObject;

    public NPCAnimator mavisAnimator;
    public NPCAnimator madelineAnimator;
    public NPCAnimator victorAnimator;

    public void Start()
    {
        StartCoroutine(autoConversation.PlayDialogue());
    }

    public void StopOpeningDialogue()
    {
        autoConversation.StopDialogue();
    }

    public void Update ()
    {
		if (autoConversation.IsFinished())
        {
            invitationObject.SetActive(true);
            mavisAnimator.changeState(NPCAnimator.CHARACTERSTATE.HAND);
            victorAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
            madelineAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);

            StartCoroutine(doorPuzzle.Hint());

            enabled = false;
        }
	}
}
