using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXEnable : MonoBehaviour {

    public GameObject objectToDisable;
    public static bool disabled = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (disabled)
            objectToDisable.SetActive(false);
        else
            objectToDisable.SetActive(true);
	}
}
