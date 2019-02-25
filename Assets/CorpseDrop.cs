using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseDrop : MonoBehaviour {

    public GameObject corpse;

	public void Drop()
    {
        corpse.SetActive(true);
    }
}
