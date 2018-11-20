using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyComponent : MonoBehaviour {


    public int TopOrBottomIndex;
    public PianoCylinderManager parentManager;
    public PianoCylinder parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("StickyComponent"))
        {
            // if(parentManager.isRotating)
            // {
            parent.AttachCylinder(other.gameObject.GetComponent<StickyComponent>().parent.gameObject, TopOrBottomIndex);
          //  }
            
        }
    }
}
