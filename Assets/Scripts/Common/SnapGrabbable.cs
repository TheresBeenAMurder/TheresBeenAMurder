using UnityEngine;

public class SnapGrabbable : OVRGrabbable
{
    public bool isInDropZone;
    public bool isUnsnappable;
    public Transform snapTransform;

    private Rigidbody thisRB;

    private bool isSnapped = false;


    protected override void Start()
    {
        base.Start();
        thisRB = GetComponent<Rigidbody>();
        thisRB.isKinematic = true;

    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if(!isSnapped && isInDropZone)
        {
            //snap it
            if (snapTransform != null)
            {
                transform.position = snapTransform.position;
                transform.rotation = snapTransform.rotation;
                isSnapped = true;
                thisRB.isKinematic = true;
            }
            else
            {
               // thisRB.useGravity = true;
            }
        }
        else if(!isSnapped)
        {
            thisRB.isKinematic = true;
            //thisRB.useGravity = true;
        }

        base.GrabEnd(Vector3.zero, Vector3.zero);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (!isSnapped || isUnsnappable) // can only be grabbed if hasn't been snapped OR if it's unsnappable
        {
            //thisRB.useGravity = false;
            //thisRB.isKinematic = true;
            base.GrabBegin(hand, grabPoint);
            isSnapped = false;
        }
    }
}
