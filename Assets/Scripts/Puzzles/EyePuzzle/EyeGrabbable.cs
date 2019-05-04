using UnityEngine;

public class EyeGrabbable : OVRGrabbable
{
    public bool nearCamera = false;
    public PuzzleCamera puzzleCamera;
    bool hasBeenGrabbed = false;
    public Transform eyeSpot;

    private void Update()
    {
        if(!hasBeenGrabbed)
        {
            transform.position = eyeSpot.transform.position;
            transform.rotation = eyeSpot.transform.rotation;
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        if(!hasBeenGrabbed)
        {
            hasBeenGrabbed = true;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);

        if (nearCamera)
        {
            puzzleCamera.eyeAttached = true;
            puzzleCamera.ToggleSnappedEye(true);
            Destroy(gameObject);
        }
    }
}
