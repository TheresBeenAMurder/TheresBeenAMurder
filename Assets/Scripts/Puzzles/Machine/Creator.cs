using System;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public AudioSource archiveAudio;
    public GameObject keyObjectPrefab;
    public ValidKeys[] validKeys;

    private Dictionary<int, AudioClip> _validKeys = new Dictionary<int, AudioClip>(); 

    public void CreateKey(int id)
    {
        AudioClip soundFile;
        if (_validKeys.TryGetValue(id, out soundFile))
        {
            SpawnObject(id, soundFile);
        }
    }

    private void SpawnObject(int id, AudioClip sound)
    {
        // Spawn object and shift it so it's sitting on top of the creator
        GameObject keyObj = Instantiate(keyObjectPrefab, transform);
        keyObj.transform.localPosition += new Vector3(0, 5, 0);

        // Set up the key properties
        ArchiveKey archiveKey = keyObj.GetComponent<ArchiveKey>();
        archiveKey.ID = id;
        archiveKey.audioSource = archiveAudio;
        archiveKey.audioClip = sound;
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
    public int key;
    public AudioClip value;
}
