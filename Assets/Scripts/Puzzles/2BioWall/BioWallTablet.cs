using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioWallTablet : OVRGrabbable {

    public BioWallScreen screen;


    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        screen.grabbingHand = hand.gameObject;
    }
}
