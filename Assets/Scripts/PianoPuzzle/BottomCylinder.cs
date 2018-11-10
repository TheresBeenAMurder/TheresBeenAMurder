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
        collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(.2f, .1f, .2f);
        resizeBoxCollider();

    }

    public void addCylinder()
    {
        cylinderCount += 1;
        collider.center = new Vector3(collider.center.x, collider.center.y + cylinderHeight / 2, collider.center.z);
        resizeBoxCollider();

    }

    public void removeCylinder()
    {

        cylinderCount -= 1;
        collider.center = new Vector3(collider.center.x, collider.center.y - cylinderHeight / 2, collider.center.z);
        resizeBoxCollider();
    }
	
    public void resizeBoxCollider()
    {

        collider.size = new Vector3(collider.size.x, cylinderCount * cylinderHeight, collider.size.z);

    }

}
