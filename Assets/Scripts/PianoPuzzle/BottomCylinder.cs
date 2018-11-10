using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCylinder : MonoBehaviour {


    int cylinderCount =1;
    BoxCollider collider;

    public float cylinderHeight;

	// Use this for initialization
	void Start () {
        cylinderCount = GetComponentsInChildren<PianoCylinder>().Length + 1;
        gameObject.AddComponent<BoxCollider>();
        collider = GetComponent<BoxCollider>();
        resizeBoxCollider();

    }

    public void addCylinder()
    {
        cylinderCount += 1;
        resizeBoxCollider();

    }

    public void removeCylinder()
    {

        cylinderCount -= 1;
        resizeBoxCollider();
    }
	
    public void resizeBoxCollider()
    {

        collider.size = new Vector3(collider.size.x, cylinderCount * cylinderHeight, collider.size.z);

    }

}
