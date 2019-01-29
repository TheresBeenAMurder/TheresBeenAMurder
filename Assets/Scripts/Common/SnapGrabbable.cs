using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapGrabbable : OVRGrabbable {

    public bool isInDropZone;
    public bool isUnsnappable;
    private bool isSnapped =false;

    public Transform snapTransform;


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
            }
        }

        base.GrabEnd(linearVelocity, angularVelocity);

    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (!isSnapped || isUnsnappable) // can only be grabbed if hasn't been snapped OR if it's unsnappable
        {
            base.GrabBegin(hand, grabPoint);
            isSnapped = false;
        }

    }

   


}
