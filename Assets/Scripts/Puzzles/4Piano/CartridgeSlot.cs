using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeSlot : MonoBehaviour {

    public PianoCartridge parent;

   public bool hasChild = false;
   public GameObject child;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {

        CartridgeDisc cd = other.GetComponent<CartridgeDisc>();
        if (cd != null  && cd.grabbedBy != null)
        {
            if (cd.gameObject == child)
            {
                child = null;
                other.transform.parent = null;
                hasChild = false;
                cd.isInDropZone = false;
                parent.removeDisc(gameObject.name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CartridgeDisc cd = other.GetComponent<CartridgeDisc>();

        if(cd != null)
        {
            if(child == null)
            {
                //hasChild = true;
                cd.snapTransform = transform;
                cd.isInDropZone = true;

                cd.cs = this;
                //child = cd.gameObject;
            }
            
        }
    }
}
