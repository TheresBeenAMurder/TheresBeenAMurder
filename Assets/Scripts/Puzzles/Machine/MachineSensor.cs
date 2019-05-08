using UnityEngine;

public class MachineSensor : MonoBehaviour
{


    public Transform snapTransformSpot;
    public AudioSource snapSound;

   // [HideInInspector]
    public MachineKey currentKey;


    // True if there's a machine key on the sensor, false otherwise
	public bool ContainsKey()
    {
        return (currentKey != null);
    }

    //public void OnCollisionExit(Collision collision)
    //{
    //    if (currentKey != null)
    //    {
    //        MachineKey key = collision.gameObject.GetComponent<MachineKey>();

    //        if (key != null && key == currentKey)
    //        {
    //            currentKey = null;
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (currentKey != null)
        {
            MachineKey key = other.gameObject.GetComponent<MachineKey>();

            if (key != null && key == currentKey)
            {
                currentKey = null;
                snapSound.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentKey == null)
        {
            MachineKey key = other.gameObject.GetComponent<MachineKey>();

            if (key != null)
            {
                currentKey = key;
            }

            SnapGrabbable sp = other.gameObject.GetComponent<SnapGrabbable>();
            if (sp != null)
            {
                sp.snapTransform = snapTransformSpot;
                sp.isInDropZone = true;
                snapSound.Play();
            }
        }
    }

    //public void OnCollisionStay(Collision collision)
    //{
    //    if (currentKey == null)
    //    {
    //        MachineKey key = collision.gameObject.GetComponent<MachineKey>();

    //        if (key != null)
    //        {
    //            currentKey = key;
    //        }
    //    }
    //}
}
