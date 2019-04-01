using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseDrop : MonoBehaviour {

    public SoundtrackManager sm;
    public GameObject corpse;

	public void Drop()
    {
        corpse.SetActive(true);
        sm.nextTrack();
    }
}
