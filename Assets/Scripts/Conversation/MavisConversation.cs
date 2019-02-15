using System.Collections;
using UnityEngine;

public class MavisConversation : MonoBehaviour
{
    public AudioClip callOut;

    private AudioSource audioSource;
    private NPC mavis;

    public IEnumerator AfterWalls()
    {
        yield return new WaitForSeconds(60);

        audioSource.clip = callOut;
        audioSource.Play();

        mavis.UpdateNextPrompt(22);
    }

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        mavis = gameObject.GetComponent<NPC>();
    }
}
