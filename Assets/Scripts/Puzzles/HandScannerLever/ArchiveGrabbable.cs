using UnityEngine;

public class ArchiveGrabbable : OVRGrabbable
{
    public bool isBeingSolved = false;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (!isBeingSolved)
        {
            base.GrabBegin(hand, grabPoint);
        }
    }
}
