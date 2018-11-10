using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderVending : MonoBehaviour {

    public GameObject[] cylinderPrefabs;

    public Transform outputLocation;


	// Use this for initialization
	void Start () {
		
	}

    public void vendCylinder(int colorToVend)
    {
        GameObject vendedCylinder = Instantiate(cylinderPrefabs[colorToVend]);
        vendedCylinder.transform.position = outputLocation.position;



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
