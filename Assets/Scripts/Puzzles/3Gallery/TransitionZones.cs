using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZones : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("GhostHand"))
        {

            other.GetComponent<GhostHand>().isTransition = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GhostHand"))
        {

            other.GetComponent<GhostHand>().isTransition = false;

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
