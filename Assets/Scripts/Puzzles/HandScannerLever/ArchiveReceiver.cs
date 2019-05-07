using System.Collections;
using UnityEngine;

public class ArchiveReceiver : MonoBehaviour
{
    private GameObject currentKey;

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
        if (currentKey != null)
        {
            currentKey.GetComponent<ArchiveKey>().Solve();
            StartCoroutine(UnlockDoor(currentKey.GetComponent<ArchiveKey>().audioSource));
        }
    }

    private IEnumerator UnlockDoor(AudioSource audio)
    {
        while (audio.isPlaying)
        {
            yield return new WaitForSeconds(audio.clip.length - audio.time);
        }
    }
}
