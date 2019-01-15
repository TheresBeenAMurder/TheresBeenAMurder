using UnityEngine;

public class PianoCylinderManager : MonoBehaviour
{
    public bool isRotating = false;
    public GameObject leftHand;
    public PianoCylinder leftHandGrabbing = null;
    public GameObject rightHand;
    public PianoCylinder rightHandGrabbing = null;
    public float rotationTolerance;

    private float leftDifference;
    private float prevLeftRotation;
    private float prevRightRotation;
    private float rightDifference;

    void Start ()
    {
        if (leftHand.activeSelf)
        {
            prevLeftRotation = leftHand.transform.rotation.x * Mathf.Rad2Deg;
        }
        if (rightHand.activeSelf)
        {
            prevRightRotation = rightHand.transform.rotation.x * Mathf.Rad2Deg;
        }
    }

    void Update()
    {
        float currLeft = prevLeftRotation;
        float currRight = prevRightRotation;

        if (leftHand.activeSelf)
        {
            currLeft = leftHand.transform.rotation.x * Mathf.Rad2Deg;
        }
        if (rightHand.activeSelf)
        {
            currRight = rightHand.transform.rotation.x * Mathf.Rad2Deg;
        }

        leftDifference = currLeft - prevLeftRotation;
        rightDifference = currRight - prevRightRotation;

        if (isRotating)
        {
            if (leftHandGrabbing == null ||
                rightHandGrabbing == null ||
                Mathf.Abs(leftDifference - rightDifference) < rotationTolerance)
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
