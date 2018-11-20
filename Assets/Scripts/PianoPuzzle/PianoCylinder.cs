using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoCylinder : OVRGrabbable {


    //COLORS:
    //None - 0
    //Red - 1
    //Blue - 2
    //Yellow - 3
    //Green - 4
    //Purple - 5
    //Orange - 6
    //

    public int color;

    public GameObject leftHand;
    public GameObject rightHand;

    public PianoCylinderManager parent;

    public GameObject stickyTop;
    public GameObject stickyBottom;

    public PianoCylinder[] attached;

    bool grabbedByLeft;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        transform.parent = null;
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        if(hand.gameObject.name == leftHand.name)
        {
            parent.leftHandGrabbing = this;
            grabbedByLeft = true;
        }
        else if (hand.gameObject.name == rightHand.name)
        {
            grabbedByLeft = false;
            parent.rightHandGrabbing = this;

        }


        grabChild(0);
        grabChild(1);


    }

    /// <summary>
    /// Notifies the object that it has been released.
    /// </summary>
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = m_grabbedKinematic;
        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
        m_grabbedBy = null;
        m_grabbedCollider = null;

        if (grabbedByLeft)
        {

            parent.leftHandGrabbing = null;

        }
        else;
        {

            parent.rightHandGrabbing = null;
        }


    }

    public void AttachCylinder(GameObject toAttach, int index)
    {
        if(attached == null)
        {
            attached = new PianoCylinder[2];
            attached[0] = null;
            attached[1] = null;
        }

        attached[index] = toAttach.GetComponent<PianoCylinder>();


    }

    public void RemoveAttachedCylinder(int index)
    {

        if (attached == null)
        {
            attached = new PianoCylinder[2];

        }

        attached[index] = null;



    }

    void grabChild(int index)
    {
        if(attached[index] != null)
        {
            attached[index].transform.parent = gameObject.transform;
            attached[index].grabChild(index);

        }
        
    }

}
