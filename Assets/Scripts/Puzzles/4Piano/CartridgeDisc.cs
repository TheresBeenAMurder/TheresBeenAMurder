using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeDisc : OVRGrabbable {

    public bool isInDropZone;
    public Transform snapTransform;

    private bool isSnapped = false;

    public int ID;

    public CartridgeSlot cs;
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (!isSnapped && isInDropZone)
        {
            //snap it
            if (snapTransform != null)
            {
                transform.position = snapTransform.position;
                transform.rotation = snapTransform.rotation;
                transform.parent = snapTransform;
                isSnapped = true;
                cs.child = gameObject;
                cs.parent.addDisc(this, cs.gameObject.name);
                Debug.Log(cs.gameObject.name);
            }
        }
        base.GrabEnd(linearVelocity, angularVelocity);
    }

    

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        isSnapped = false;
        
        
    }
}
