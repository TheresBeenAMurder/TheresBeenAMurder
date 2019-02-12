using UnityEngine;

public class InvitationSpawner : MonoBehaviour
{
    public GameObject invitationPrefab;
    public GameObject mavis;
    public AudioSource openingDialogue;
	
	void Update ()
    {
		if (!openingDialogue.isPlaying)
        {
            Debug.Log("Spawning Object");
            GameObject invite = Instantiate(invitationPrefab, mavis.transform);
            invite.transform.position = new Vector3(0, 1, .5f);

            Destroy(gameObject);
        }
	}
}
