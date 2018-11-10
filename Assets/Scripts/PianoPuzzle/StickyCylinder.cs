using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCylinder : MonoBehaviour {
    
    //COLORS:
    //None - 0
    //Red - 1
    //Blue - 2
    //Yellow - 3
    //Green - 4
    //Purple - 5
    //Orange - 6
    //

    private GameObject attached;

    public Transform snapPoint;

    public BottomCylinder thisBottom;

    private void Start()
    {
        attached = null;
        thisBottom = GetComponentInParent<BottomCylinder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attached == null)
        {
            if (other.CompareTag("PianoCylinder"))
            {
                attachColor(other.gameObject);
                other.gameObject.transform.root.transform.position = snapPoint.position;
                other.transform.root.transform.parent = this.transform;
                if(other.GetComponent<BottomCylinder>() != null)
                {

                    Destroy(other.gameObject.GetComponent<BottomCylinder>());

                }
                other.gameObject.GetComponentInChildren<StickyCylinder>().thisBottom = thisBottom;
                thisBottom.addCylinder();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(attached != null)
        {
            if (other.CompareTag("PianoCylinder") && other.GetComponent<StickyCylinder>() != null)
            {
                
                attached = null;
                other.transform.root.transform.parent = null;
                other.gameObject.AddComponent<BottomCylinder>();
                other.gameObject.GetComponent<StickyCylinder>().thisBottom = other.gameObject.GetComponent<BottomCylinder>();
                thisBottom.removeCylinder();
            }


        }
    }

    void attachColor(GameObject toAttach)
    {
        
        attached = toAttach;
    }
}
