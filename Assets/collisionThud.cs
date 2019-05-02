using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionThud : MonoBehaviour
{


    bool thudPlayed = false;
    public AudioSource thudSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Body");
        if(collision.gameObject.CompareTag("Ellis") && !thudPlayed)
        {
            Debug.Log("THUD");
            thudSound.Play();
            thudPlayed = true;
        }
    }
}
