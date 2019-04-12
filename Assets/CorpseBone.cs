using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBone : OVRGrabbable
{


    public TeleportCorpse teleporter;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        teleporter.corpseGrabbed = true;
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        teleporter.corpseGrabbed = false;
    }
}
