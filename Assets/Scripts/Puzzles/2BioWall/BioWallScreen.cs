using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioWallScreen : MonoBehaviour {


    public GameObject grabbingHand;

    public GameObject activeHand;

	// Use this for initialization
	void Start () {
        activeHand = null;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(grabbingHand == null || other.name != grabbingHand.name)
        {
            activeHand = other.gameObject;
        }
    }

    // Update is called once per frame
    void Update () {
		if(activeHand != null)
        {

        }
	}
}
