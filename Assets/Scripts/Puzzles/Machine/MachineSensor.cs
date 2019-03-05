using UnityEngine;

public class MachineSensor : MonoBehaviour
{
    [HideInInspector]
    public MachineKey currentKey;

    // True if there's a machine key on the sensor, false otherwise
	public bool ContainsKey()
    {
        return (currentKey != null);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (currentKey != null)
        {
            MachineKey key = collision.gameObject.GetComponent<MachineKey>();

            if (key != null && key == currentKey)
            {
                currentKey = null;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (currentKey == null)
        {
            MachineKey key = collision.gameObject.GetComponent<MachineKey>();

            if (key != null)
            {
                currentKey = key;
            }
        }
    }
}
