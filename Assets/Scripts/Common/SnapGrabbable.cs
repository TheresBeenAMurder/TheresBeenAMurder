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
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity, bool useGravity = false)
    {
        if (!isSnapped && isInDropZone)
        {
            //snap it
            if (snapTransform != null)
            {
                transform.position = snapTransform.position;
                transform.rotation = snapTransform.rotation;
                isSnapped = true;

                base.GrabEnd(linearVelocity, angularVelocity, false);
                return;
            }
        }

        base.GrabEnd(linearVelocity, angularVelocity, true);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint, bool useGravity = false)
    {
        if (!isSnapped || isUnsnappable) // can only be grabbed if hasn't been snapped OR if it's unsnappable
        {
            base.GrabBegin(hand, grabPoint, true);
            isSnapped = false;
        }
    }
}
