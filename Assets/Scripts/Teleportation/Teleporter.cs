using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public GameObject teleportIndicator;
    public Transform playerGaze;
    public float teleportDistance = 50f;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        OVRInput.Update();

        // show teleport indicator in given position
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, teleportDistance))
        {
            if (hit.collider.tag == "floor")
            {
                if (!teleportIndicator.activeSelf)
                {
                    teleportIndicator.SetActive(true);
                }

                teleportIndicator.transform.position = hit.point;
            }
            else
            {
                teleportIndicator.SetActive(false);
            }
        }
        else
        {
            teleportIndicator.SetActive(false);
        }

        // teleport player to gaze location
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (teleportIndicator.activeSelf)
            {
                Vector3 indicatorPosition = teleportIndicator.transform.position;
                playerGaze.position = new Vector3(indicatorPosition.x,
                    playerGaze.position.y,
                    indicatorPosition.z);
            }
        }
	}
}
