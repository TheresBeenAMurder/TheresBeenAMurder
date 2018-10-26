using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour {

    public GameObject murderBoard;
    public GameObject rHand;

    private bool canPoint = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (murderBoard.activeSelf)
        {
            // turn on ray checking
            canPoint = true;
        }
        else
        {
            // turn off ray checking
            canPoint = false;
        }

        if (canPoint)
        {
            // check if player is pointing
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) &&
                OVRInput.Get(OVRInput.RawTouch.RThumbRest) &&
                !OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))
            {
                Debug.Log("Player is pointing");
                // throw out a ray and see if it hits the canvas
                Ray ray = new Ray(rHand.transform.position, rHand.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "MurderBoardScroll")
                    {
                        Debug.Log("Player is pointing at scroll");
                    }
                }
            }
        }
	}
}
