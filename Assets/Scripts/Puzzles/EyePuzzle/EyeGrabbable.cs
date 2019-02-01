using UnityEngine;

public class EyeGrabbable : OVRGrabbable
{
    public bool nearCamera = false;
    public PuzzleCamera puzzleCamera;

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
