using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoCylinderManager : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    public PianoCylinder leftHandGrabbing = null;
    public PianoCylinder rightHandGrabbing = null;

    public bool isRotating = false;

    private float prevLeftRotation;
    private float prevRightRotation;

    private float leftDifference;
    private float rightDifference;

    public float rotationTolerance;

	// Use this for initialization
	void Start () {
       // prevLeftRotation = leftHand.transform.localRotation.x;
       // prevRightRotation = rightHand.transform.localRotation.x;
	}
	
	// Update is called once per frame
	void Update () {

        // float currLeft = leftHand.transform.localRotation.x;
        //float currRight = rightHand.transform.localRotation.x;

        // leftDifference = currLeft - prevLeftRotation;
        // rightDifference = currRight - prevRightRotation;


        if (leftHandGrabbing == null || rightHandGrabbing == null)
        {

            isRotating = false;

        }
        //else if(Mathf.Abs(leftDifference - rightDifference) > rotationTolerance) //then we're twisting!
        // {
        else
        {
            //if the cylinders we're grabbing are attached and adjacent, separate them
            if (rightHandGrabbing.attached[0].gameObject.name == leftHandGrabbing.attached[1].gameObject.name)
            {
                rightHandGrabbing.RemoveAttachedCylinder(0,1);
                leftHandGrabbing.RemoveAttachedCylinder(1,0);
            }
            else if (rightHandGrabbing.attached[1].gameObject.name == leftHandGrabbing.attached[0].gameObject.name)
            {
                rightHandGrabbing.RemoveAttachedCylinder(1,0);
                leftHandGrabbing.RemoveAttachedCylinder(0,1);
            }
            else if (rightHandGrabbing.attached[1].gameObject.name == leftHandGrabbing.attached[1].gameObject.name)
            {
                rightHandGrabbing.RemoveAttachedCylinder(1, 1);
                leftHandGrabbing.RemoveAttachedCylinder(1, 1);
            }
            else if (rightHandGrabbing.attached[0].gameObject.name == leftHandGrabbing.attached[0].gameObject.name)
            {
                rightHandGrabbing.RemoveAttachedCylinder(0, 0);
                leftHandGrabbing.RemoveAttachedCylinder(0, 0);
            }


        }

       // }


      //  prevLeftRotation = currLeft;
       // prevRightRotation = currRight;


    }
}
