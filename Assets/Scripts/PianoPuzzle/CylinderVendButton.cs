using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderVendButton : MonoBehaviour {


    public int cylinder;

    public CylinderVending parent;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void whenPressed()
    {


        parent.vendCylinder(cylinder);

    }
}
