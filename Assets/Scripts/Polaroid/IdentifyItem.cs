using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentifyItem : MonoBehaviour {

	// Use this for initialization
	public Text currentItemFound;
	void Start () {
		currentItemFound.text = "searching";
	}
	
	// Update is called once per frame
	void Update () {
		LookForItem();
	}

	void LookForItem(){
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		//Debug.DrawRay(transform.position, fwd, Color.green);
		if (Physics.Raycast(transform.position, fwd, out hit, 100)) {
			Debug.DrawRay(transform.position, fwd * hit.distance, Color.green);
			ItemInfo itemInfo = hit.collider.gameObject.GetComponent<ItemInfo>();
			Debug.Log("Found Item");
            if(itemInfo != null){
				currentItemFound.text = itemInfo.itemName;
				Debug.Log("Looking at:"+itemInfo.itemName);
			}
			else{
				currentItemFound.text = "searching";
			}
		}
	}
}
