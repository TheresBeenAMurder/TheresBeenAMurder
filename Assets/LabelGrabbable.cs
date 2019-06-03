using UnityEngine;

public class LabelGrabbable : OVRGrabbable
{
    public bool isInDropZone;
    public bool isUnsnappable;
    public Transform snapTransform;

    public AudioSource crunchSound;

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
                crunchSound.Play();
                return;
            }
        }

        base.GrabEnd(linearVelocity, angularVelocity, false);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint, bool useGravity = false)
    {
        if (!isSnapped || isUnsnappable) // can only be grabbed if hasn't been snapped OR if it's unsnappable
        {
            base.GrabBegin(hand, grabPoint, false);
            isSnapped = false;
        }
    }
}
