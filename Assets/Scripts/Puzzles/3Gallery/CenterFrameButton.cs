using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrameButton : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    public CenterFrame parent;
    private bool done = false;

    public GameObject indicator;
    public float range = 0.3f;

    public CenterFramePivot pivot;

    private void Update()
    {
        //if (!done)
        //{
        //    if (Vector3.Distance(leftHand.transform.position, transform.position) <= range)
        //    {
        //        if (Gestures.IsGrabbing(leftHand, rightHand) != null && Gestures.IsGrabbing(leftHand, rightHand).name == leftHand.name)
        //        {

        //            buttonPressed();
        //        }
        //    }
        //    if (Vector3.Distance(rightHand.transform.position, transform.position) <= range)
        //    {
        //        if (Gestures.IsGrabbing(leftHand, rightHand) != null && Gestures.IsGrabbing(leftHand, rightHand).name == rightHand.name)
        //        {

        //            buttonPressed();
        //        }
        //    }

        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!done && other.CompareTag("GhostHand"))
        {
            Debug.Log("GO");
            buttonPressed();
            parent.isPaused = true;
            //parent.end = true;
        }
        
    }


    void buttonPressed()
    {
        done = true;
        parent.Done();
        pivot.RotateOpen();
        Debug.Log("Pivoting");
        Destroy(parent.leftGhost);
        Destroy(parent.rightGhost);
    }
}
