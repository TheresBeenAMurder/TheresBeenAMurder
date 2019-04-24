using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyThud : MonoBehaviour
{

    public AudioSource bodyAudio;
    public AudioClip thudSound;

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.CompareTag("ThudObject"))
        {
            bodyAudio.clip = thudSound;
            bodyAudio.Play();
            Debug.Log("Thud");
        }
    }
}
