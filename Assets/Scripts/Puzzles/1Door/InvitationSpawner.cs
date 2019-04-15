using UnityEngine;

public class InvitationSpawner : MonoBehaviour
{
    public Door doorPuzzle;
    public GameObject invitationPrefab;
    public GameObject mavis;
    public AudioSource openingDialogue;
    public GameObject invitationObject;

    public NPCAnimator mavisAnimator;

    public NPCAnimator madelineAnimator;
    public NPCAnimator victorAnimator;

	
	void Update ()
    {
		if (!openingDialogue.isPlaying)
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
