using UnityEngine;

public class PuzzleCamera : MonoBehaviour
{
    public Camera cameraView;
    public GameObject snappedEye;

    [HideInInspector]
    public bool eyeAttached = false;

    // everythingCameraLayer includes OnlyVisibleToEye layer
    private int everythingCameraLayer = -1;
    private int originalCameraLayer;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Eye"))
        {
            SwitchCameraLayers(everythingCameraLayer);
            other.gameObject.GetComponent<EyeGrabbable>().nearCamera = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Eye") && !eyeAttached)
        {
            SwitchCameraLayers(originalCameraLayer);
            other.gameObject.GetComponent<EyeGrabbable>().nearCamera = false;
        }
    }

    public void Start()
    {
        // turn off the snapped eye object at the start
        ToggleSnappedEye(false);

        originalCameraLayer = cameraView.cullingMask;
    }

    // Updates the layers the camera can see
    public void SwitchCameraLayers(int newLayer)
    {
        cameraView.cullingMask = newLayer;
    }

    public void ToggleSnappedEye(bool onOff)
    {
        snappedEye.SetActive(onOff);
    }
}
