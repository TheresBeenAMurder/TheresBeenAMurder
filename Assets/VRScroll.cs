using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRScroll : MonoBehaviour {

    GameObject hand;

    public string handName;

    bool isTouching = false;

    public Transform content;

    Vector3 oldPos;
    Vector3 newPos;

    float minY;
    float maxY;

	// Use this for initialization
	void Start () {
        hand = GameObject.Find(handName);
	}
	
	// Update is called once per frame
	void Update () {
		if(isTouching)
        {
            newPos = new Vector3(0,hand.transform.position.y, 0);
            Vector3 change = newPos - oldPos;
            Vector3 contentAdjustment = content.position + change;

            if(contentAdjustment.y > minY && contentAdjustment.y < maxY)
            {

                content.position = contentAdjustment;

            }

        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == handName)
        {
            isTouching = true;
            oldPos = new Vector3(0, other.gameObject.transform.position.y, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == handName)
        {
            isTouching = false;

        }
    }
}
