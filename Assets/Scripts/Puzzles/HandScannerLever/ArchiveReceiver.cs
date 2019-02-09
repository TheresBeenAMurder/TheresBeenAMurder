using UnityEngine;

public class ArchiveReceiver : MonoBehaviour
{
    public Rigidbody door;

    private GameObject currentKey;

    ////private bool DoorLocked()
    ////{
    ////    return door.isKinematic;
    ////}

    public void OnTriggerEnter(Collider other)
    {
        ArchiveKey key = other.gameObject.GetComponent<ArchiveKey>();
        if (currentKey == null && key != null)
        {
            currentKey = other.gameObject;
            SnapGrabbable cannister = other.gameObject.GetComponent<SnapGrabbable>();

            if (cannister != null)
            {
                cannister.snapTransform = transform;
                cannister.isInDropZone = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject == currentKey)
        {
            SnapGrabbable cannister = other.gameObject.GetComponent<SnapGrabbable>();
            if (cannister != null)
            {
                cannister.isInDropZone = false;
            }

            currentKey = null;
        }
    }

    public void Solve()
    {
        ////ToggleDoorLock();
        currentKey.GetComponent<ArchiveKey>().Solve();
    }

    private void ToggleDoorLock()
    {
        ////door.isKinematic = !door.isKinematic;
    }

    public void Update()
    {
        // key is done playing, unlock the door
        ////if (DoorLocked() && !currentKey.GetComponent<ArchiveKey>().audioSource.isPlaying)
        ////{
        ////    ToggleDoorLock();
        ////}
    }
}
