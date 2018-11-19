using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour {


    public LineDrawHandler parent;

    public DrawBWPoints currentLine;

    public GameObject linePrefab;

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject currentlyDrawingHand;

    private List<DrawBWPoints> photoLines;

	// Use this for initialization
	void Start () {
        parent = GameObject.FindObjectOfType<LineDrawHandler>();
        leftHand = GameObject.Find("hand_left");
        rightHand = GameObject.Find("hand_right");
        currentLine = null;
	}

    private void Update()
    {
        if (currentlyDrawingHand != null)
        {
            if (currentlyDrawingHand.name == rightHand.name)
            {
                if (currentLine == null) //the player isn't currently drawing a line
                {

                    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && Gestures.IsGrabbing(leftHand, rightHand) == null)
                    {
                        spawnLine(rightHand);
                    }

                }

                else // the player is dragging through and we should snap the line to the photo but not reparent
                {
                    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && Gestures.IsGrabbing(leftHand, rightHand) == null)
                    {
                        snapToPhoto();
                    }
                }

            }
            else if (currentlyDrawingHand.name == leftHand.name)
            {
                if (currentLine == null) //the player isn't currently drawing a line
                {
                    if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && Gestures.IsGrabbing(leftHand, rightHand) == null)
                    {
                        spawnLine(leftHand);
                    }

                }
                else // the player is dragging through and we should snap the line to the photo but not reparent
                {
                    if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && Gestures.IsGrabbing(leftHand, rightHand) == null)
                    {
                        snapToPhoto();
                    }
                }


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //RIGHT
        if (other.gameObject.name == rightHand.name)
        {
            currentlyDrawingHand = rightHand;
            
        }


        //LEFT
        else if (other.gameObject.name == leftHand.name)
        {
            currentlyDrawingHand = leftHand;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == rightHand.name && currentlyDrawingHand != null && rightHand.name == currentlyDrawingHand.name)
        {
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) // the line hasnt been released
            {
                unsnapToPhoto();

            }
            currentlyDrawingHand = null;
        }

        else if (other.gameObject.name == leftHand.name && currentlyDrawingHand != null && leftHand.name == currentlyDrawingHand.name)
        {
            if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) // the line hasnt been released
            {
                unsnapToPhoto();

            }

            currentlyDrawingHand = null;
        }


    }

    public void purgeLines()
    {
        if (photoLines != null)
        {
            foreach (DrawBWPoints dbw in photoLines)
            {


                Destroy(dbw.gameObject);

            }
        }
    }

    void snapToPhoto()
    {
        currentLine.pointsToDraw[1] = gameObject.transform;


    }

    void unsnapToPhoto()
    {
        currentLine.pointsToDraw[1] = currentlyDrawingHand.transform;


    }

    void spawnLine(GameObject hand)
    {
        GameObject currLine = Instantiate(linePrefab, gameObject.transform);
        currentLine = currLine.GetComponent<DrawBWPoints>();
        currentLine.pointsToDraw[0] = gameObject.transform;
        currentLine.pointsToDraw[1] = hand.transform;

        photoLines.Add(currentLine);

    }
}
