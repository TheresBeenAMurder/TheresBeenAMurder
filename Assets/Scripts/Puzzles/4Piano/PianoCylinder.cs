using UnityEngine;
using System.Collections.Generic;

public class PianoCylinder : OVRGrabbable
{
    //COLORS:
    //None - 0
    //Red - 1
    //Blue - 2
    //Yellow - 3
    //Green - 4
    //Purple - 5
    //Orange - 6
    //

    public PianoCylinder[] attached;
    public int color;
    public bool grabbedByLeft;
   // public GameObject leftHand;
    public float maxDistanceToSeparate;
    public PianoCylinderManager parent;
  //  public GameObject rightHand;
    public GameObject stickyBottom;
    public GameObject stickyTop;
  //  private ConfigurableJoint [] childJoints;
    

    

    public void AttachCylinder(GameObject toAttach, int index, int otherIndex)
    {
        if (attached == null || attached.Length == 0)
        {
            attached = new PianoCylinder[2];
            attached[0] = null;
            attached[1] = null;
        }
        attached[index] = toAttach.GetComponent<PianoCylinder>();
        if (toAttach.GetComponent<PianoCylinder>().attached.Length == 0)
        {
            toAttach.GetComponent<PianoCylinder>().attached = new PianoCylinder[2];
            toAttach.GetComponent<PianoCylinder>().attached[0] = null;
            toAttach.GetComponent<PianoCylinder>().attached[1] = null;
        }
        toAttach.GetComponent<PianoCylinder>().attached[otherIndex] = this;
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        if(hand.gameObject.name == "LeftHandAnchor")
        {
            parent.leftHandGrabbing = this;
            grabbedByLeft = true;
        }
        else if (hand.gameObject.name == "RightHandAnchor")
        {
            grabbedByLeft = false;
            parent.rightHandGrabbing = this;

        }

        GrabChild(0);
        GrabChild(1);
    }

    public void GrabChild(int index)
    {
        if (attached.Length > 0)
        {
            if (attached[index] != null)
            {
                attached[index].transform.parent = gameObject.transform;
               
                if (attached[index].attached[0] != null && attached[index].attached[0].name == gameObject.name)
                {
                    attached[index].GrabChild(1);
                    
                }
                else if (attached[index].attached[1] != null && attached[index].attached[1].name == gameObject.name)
                {
                    attached[index].GrabChild(0);
                    
                }

            }
        }
    }

    public Rigidbody objectRigidbody()
    {
        return GetComponent<Rigidbody>();
    }

    
    // Notifies the object that it has been released.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {



        bool snapped = false;
        List<string> childNames = new List<string>();
        childNames.Clear();
        foreach (PianoCylinder childCyl in GetComponentsInChildren<PianoCylinder>())
        {
            childNames.Add(childCyl.gameObject.name);
        }
        if (attached != null && attached.Length > 0)
        {
            if (attached[0] != null)
            {
                if (!childNames.Contains(attached[0].gameObject.name))
                {
                    //then we gotta snap to it
                    if (attached[0].attached[0] != null && attached[0].attached[0].gameObject.name == gameObject.name)
                    {
                        transform.position = attached[0].stickyBottom.GetComponent<StickyComponent>().snapPoint.transform.position;
                        transform.rotation = attached[0].stickyBottom.GetComponent<StickyComponent>().snapPoint.transform.rotation;
                        snapped = true;
                    }
                    else if (attached[0].attached[1] != null && attached[0].attached[1].gameObject.name == gameObject.name)
                    {
                        transform.position = attached[0].stickyTop.GetComponent<StickyComponent>().snapPoint.transform.position;
                        transform.rotation = attached[0].stickyTop.GetComponent<StickyComponent>().snapPoint.transform.rotation;
                        snapped = true;
                    }
                }

            }
            if (attached[1] != null && !snapped)
            {
                if (!childNames.Contains(attached[1].gameObject.name))
                {
                    //then we gotta snap to it
                    if (attached[1].attached[0] != null && attached[1].attached[0].gameObject.name == gameObject.name)
                    {
                        transform.position = attached[1].stickyBottom.GetComponent<StickyComponent>().snapPoint.transform.position;
                        transform.rotation = attached[1].stickyBottom.GetComponent<StickyComponent>().snapPoint.transform.rotation;
                        snapped = true;
                    }
                    else if (attached[1].attached[1] != null && attached[1].attached[1].gameObject.name == gameObject.name)
                    {
                        transform.position = attached[1].stickyTop.GetComponent<StickyComponent>().snapPoint.transform.position;
                        transform.rotation = attached[1].stickyTop.GetComponent<StickyComponent>().snapPoint.transform.rotation;
                        snapped = true;
                    }
                }

            }
        }

        //clearJoints();
        foreach (PianoCylinder child in gameObject.GetComponentsInChildren<PianoCylinder>())
        {
            child.gameObject.transform.parent = null;
            //child.clearJoints();
        }

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
        else
        {
            parent.rightHandGrabbing = null;
        }


        

    } 
    
    public void snapAttached(int index)
    {
        if(index == 0)
        {
            attached[index].transform.position = stickyBottom.GetComponent<StickyComponent>().snapPoint.position;
        }
        if (index == 1)
        {
            attached[index].transform.position = stickyTop.GetComponent<StickyComponent>().snapPoint.position;
        }
    }

    
    

    public void RemoveAttachedCylinder(int index, int otherIndex)
    {
        Debug.Log("Removing " + index + " from " + gameObject.name);

        if (attached.Length == 0)
        {
            attached = new PianoCylinder[2];
        }


        attached[index].attached[otherIndex] = null;
        attached[index] = null;
    }
}
