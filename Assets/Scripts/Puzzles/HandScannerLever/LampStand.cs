using UnityEngine;

public class LampStand : MonoBehaviour
{
    public SnapGrabbable lamp;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("dnaLamp"))
        {
            lamp.isInDropZone = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("dnaLamp"))
        {
            lamp.isInDropZone = false;
        }
    }
}
