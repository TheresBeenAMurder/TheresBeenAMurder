using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Takes 3 objects and combines them to create a key for
// the archives.
public class Combiner : MonoBehaviour
{
    public Creator creator;

    private List<MachineKey> currentKeys = new List<MachineKey>();

    private void CheckKeys()
    {
        if (currentKeys.Count != 3)
        {
            return;
        }

        // Currently has 3 keys, time to combine/create
        int combined = CombineKeys();
        StartCoroutine(Clear(combined));
    }

    private IEnumerator Clear(int combined)
    {
        yield return new WaitForSeconds(1);

        foreach (MachineKey key in currentKeys)
        {
            // Destroys game object associated
            key.Solve();
        }
        currentKeys.Clear();
        creator.CreateKey(combined);
    }

    private int CombineKeys()
    {
        List<int> keyIDs = new List<int>();
        foreach (MachineKey key in currentKeys)
        {
            keyIDs.Add(key.ID);
        }

        int combined = 0;
        for (int i = 2; i >= 0; i--)
        {
            int min = keyIDs.Min();
            combined += min * (int)Mathf.Pow(10f, i);
            keyIDs.Remove(min);
        }

        // Combined key is 3 digits, smallest number to largest, duplicate
        // numbers are possible
        return combined;
    }

    public void OnCollisionExit(Collision collision)
    {
        MachineKey key = collision.gameObject.GetComponent<MachineKey>();
        if (key != null)
        {
            if (currentKeys.Contains(key))
            {
                currentKeys.Remove(key);
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        MachineKey key = collision.gameObject.GetComponent<MachineKey>();
        if (key != null)
        {
            if (!currentKeys.Contains(key))
            {
                currentKeys.Add(key);
                CheckKeys();
            }
        }
    }
}