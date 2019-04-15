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
    }

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        mavis = gameObject.GetComponent<NPC>();
    }
}
