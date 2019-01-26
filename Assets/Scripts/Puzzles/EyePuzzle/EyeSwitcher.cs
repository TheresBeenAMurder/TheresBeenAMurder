using UnityEngine;

public class EyeSwitcher : MonoBehaviour
{
    public Camera eyeToSwitch;

    private int oldCameraLayers;
    private int everythingCameraLayer = -1;

	public void Start ()
    {
        // Store old mask (Everything - OnlyVisibleToEye)
        oldCameraLayers = eyeToSwitch.cullingMask;
	}
	
    public void SwitchEye()
    {
        if (eyeToSwitch.cullingMask == oldCameraLayers)
        {
            eyeToSwitch.cullingMask = everythingCameraLayer;
        }
        else
        {
            eyeToSwitch.cullingMask = oldCameraLayers;
        }
    }
	
	public void Update ()
    {
		if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            SwitchEye();
        }
	}
}
