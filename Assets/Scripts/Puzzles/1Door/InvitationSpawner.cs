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
            //GameObject invite = Instantiate(invitationPrefab, mavis.transform);

            //invite.transform.localPosition = new Vector3(0, 1, .5f);
            // invite.transform.parent = null;

            invitationObject.SetActive(true);
            mavisAnimator.changeState(NPCAnimator.CHARACTERSTATE.HAND);
            victorAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
            madelineAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);

            StartCoroutine(doorPuzzle.Hint());

            enabled = false;
        }
	}
}
