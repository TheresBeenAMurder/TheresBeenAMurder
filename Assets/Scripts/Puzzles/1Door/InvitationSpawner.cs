using UnityEngine;

public class InvitationSpawner : MonoBehaviour
{
    public Door doorPuzzle;
    public GameObject invitationPrefab;
    public GameObject mavis;
    public AudioSource openingDialogue;
	
	void Update ()
    {
		if (!openingDialogue.isPlaying)
        {
            GameObject invite = Instantiate(invitationPrefab, mavis.transform);
            invite.transform.localPosition = new Vector3(0, 1, .5f);
            invite.transform.parent = null;

            StartCoroutine(doorPuzzle.Hint());

            enabled = false;
        }
	}
}
