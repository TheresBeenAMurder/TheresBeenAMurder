using System;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public Combiner combiner;
    public ValidKeys[] validKeys;

    private GameObject currentCanister;
    private Dictionary<string, AudioClip> _validKeys = new Dictionary<string, AudioClip>();

    public AudioSource containerSnap;

    // Returns true if the cannister is empty and can be filled with a new key
    private bool CanisterEmpty()
    {
        if (currentCanister != null)
        {
            return (currentCanister.GetComponent<ArchiveKey>().ID.Equals(""));
        }

        return false;
    }

    // Returns true if succeeded in creating a key
    public bool CreateKey(string id)
    {
        AudioClip soundFile;
        if (CanisterEmpty())
        {
            if (_validKeys.TryGetValue(id, out soundFile))
            {
                currentCanister.GetComponent<ArchiveKey>().Fill(id, soundFile);
                return true;
            }
        }

        return false;
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject != null && collider.gameObject == currentCanister)
        {
            currentCanister = null;

            SnapGrabbable canister = collider.gameObject.GetComponent<SnapGrabbable>();
            if (canister != null)
            {
                canister.isInDropZone = false;
            }
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (currentCanister == null && collider.gameObject != null)
        {
            ArchiveKey key = collider.gameObject.GetComponent<ArchiveKey>();
            if (key != null)
            {
                currentCanister = collider.gameObject;

                SnapGrabbable canister = collider.gameObject.GetComponent<SnapGrabbable>();

                if (canister != null)
                {
                    canister.snapTransform = transform;
                    canister.isInDropZone = true;
                    containerSnap.Play();
                }
            }
        }
    }

    public void Start()
    {
        // Because Unity is dumb
        foreach (ValidKeys keyValuePair in validKeys)
        {
            _validKeys.Add(keyValuePair.key, keyValuePair.value);
        }
    }
}

[Serializable]
public struct ValidKeys
{
    public string key;
    public AudioClip value;
}
