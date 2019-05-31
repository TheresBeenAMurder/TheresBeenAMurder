using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiowallTablet : OVRGrabbable {


    public RectTransform content;

    public float minX;
    public float maxX;

    public GameObject leftHand;
    public GameObject rightHand;

    bool grabbedLeft = false;
    bool grabbedRight = false;

    public GameObject activeHand;

    float prevX;
    float currentX;
    public float multiplier;
    

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint, bool useGravity = false)
    {
        base.GrabBegin(hand, grabPoint);

        if (hand.gameObject.name.Contains("left"))
        {
            grabbedLeft = true;
        }
        else
        {
            grabbedRight = true;
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity, bool useGravity = false)
    {
        base.GrabEnd(linearVelocity, angularVelocity);

        if(grabbedRight && grabbedLeft)
        {
            if (Gestures.IsGrabbing(leftHand, rightHand).name.ToLower().Contains("left"))
            {
                //then the left hand is still holding it
                grabbedRight = false;
            }
            else
            {
                grabbedLeft = false;
            }
        }
        else if (grabbedRight)
        {
            grabbedRight = false;
        }
        else if (grabbedLeft)
        {
            grabbedLeft = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activeHand == null)
        {
            if (other.name == "LeftHandAnchor")
            {
                activeHand = leftHand;

            }
            else if (other.name == "RightHandAnchor")
            {
                activeHand = rightHand;
            }

            if(activeHand != null)
            {
                prevX = activeHand.transform.localPosition.x;
                currentX = activeHand.transform.localPosition.x;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.name == "LeftHandAnchor" || other.name == "RightHandAnchor") && activeHand != null)
        {
            activeHand = null;

        }
    }

        // Update is called once per frame
    void Update () {
        if(activeHand != null)
        {

            currentX = activeHand.transform.localPosition.x;

            float difference = currentX - prevX;
            difference *= multiplier;

            prevX = currentX;


            if (content.localPosition.x + difference >= minX && content.localPosition.x + difference <= maxX)
            {
                content.localPosition += new Vector3(difference, 0, 0);
            }
            else if (content.localPosition.x + difference < minX)
            {
                content.localPosition = new Vector3(minX, 0, 0);
            }
            else if (content.localPosition.x + difference > maxX)
            {
                content.localPosition = new Vector3(maxX, 0, 0);
            }


        }
		
	}
}
