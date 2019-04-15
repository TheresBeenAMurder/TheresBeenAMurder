using System.Collections;
using UnityEngine;

public class VictorConversation : MonoBehaviour
{
    private AudioSource audioSource;
    private NPC victor;

    public IEnumerator AfterWalls()
    {
        yield return new WaitForSeconds(65);
    }

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        victor = gameObject.GetComponent<NPC>();
    }
}
