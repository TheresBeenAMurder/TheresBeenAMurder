using UnityEngine;

public class ArchiveReceiver : MonoBehaviour
{
    public ArchiveGrabbable doorHandle;

    private GameObject currentKey;

    private bool DoorLocked()
    {
        return doorHandle.isBeingSolved;
    }

    public void OnTriggerEnter(Collider other)
    {
        ArchiveKey key = other.gameObject.GetComponent<ArchiveKey>();
        if (currentKey == null && key != null)
        {
            currentKey = other.gameObject;
            SnapGrabbable canister = other.gameObject.GetComponent<SnapGrabbable>();

            if (canister != null)
            {
                canister.snapTransform = transform;
                canister.isInDropZone = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject == currentKey)
        {
            SnapGrabbable canister = other.gameObject.GetComponent<SnapGrabbable>();
            if (canister != null)
            {
                canister.isInDropZone = false;
            }

            currentKey = null;
        }
    }

    public void Solve()
    {
        // Door locking still doesn't work
        //ToggleDoorLock();
        currentKey.GetComponent<ArchiveKey>().Solve();
    }

    private void ToggleDoorLock()
    {
        doorHandle.isBeingSolved = !doorHandle.isBeingSolved;
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
