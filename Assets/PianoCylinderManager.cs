using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoCylinderManager : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    //public Material handMat;

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
        if (leftHand.activeSelf == true)
        {
            prevLeftRotation = leftHand.transform.rotation.x * Mathf.Rad2Deg;
        }
        if (rightHand.activeSelf == true)
        {
            prevRightRotation = rightHand.transform.rotation.x * Mathf.Rad2Deg;
        }
    }

    // Update is called once per frame
    void Update() {


        float currLeft = prevLeftRotation;
        float currRight = prevRightRotation;

        if (leftHand.activeSelf == true)
        {
            currLeft = leftHand.transform.rotation.x * Mathf.Rad2Deg;
        }
        if (rightHand.activeSelf == true)
        {
            currRight = rightHand.transform.rotation.x * Mathf.Rad2Deg;
        }

        leftDifference = currLeft - prevLeftRotation;
        rightDifference = currRight - prevRightRotation;


        


        if (isRotating)
        {
            if (leftHandGrabbing == null || rightHandGrabbing == null || Mathf.Abs(leftDifference - rightDifference) < rotationTolerance)
            {
                    isRotating = false;
                
            }
        }
        else
        {

            if (Mathf.Abs(leftDifference - rightDifference) >= rotationTolerance)
            {
                if (leftHandGrabbing != null && rightHandGrabbing != null)
                {
                   
                        isRotating = true;
                   
                }
            }
        }
        


        prevLeftRotation = currLeft;
        prevRightRotation = currRight;


    }
}
