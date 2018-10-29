using UnityEngine;

public static class Gestures
{ 
    // returns the hand the player is grabbing with or null if not grabbing
    // right has precedence over left
    public static GameObject IsGrabbing(GameObject leftHand, GameObject rightHand)
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) &&
                OVRInput.Get(OVRInput.RawTouch.RThumbRest) &&
                OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))
        {
            return rightHand;
        }
        else if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) &&
                OVRInput.Get(OVRInput.RawTouch.LThumbRest) &&
                OVRInput.Get(OVRInput.RawTouch.LIndexTrigger))
        {
            return leftHand;
        }

        return null;
    }

    // returns the hand the player is pointing with or null if not pointing
    // right has precedence over left
    public static GameObject IsPointing(GameObject leftHand, GameObject rightHand)
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) &&
                OVRInput.Get(OVRInput.RawTouch.RThumbRest) &&
                !OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))
        {
            return rightHand;
        }
        else if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) &&
                OVRInput.Get(OVRInput.RawTouch.LThumbRest) &&
                !OVRInput.Get(OVRInput.RawTouch.LIndexTrigger))
        {
            return leftHand;
        }

        return null;
    }
}
