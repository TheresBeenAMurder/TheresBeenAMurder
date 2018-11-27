using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {
    

    public Vector3 Offset = new Vector3(0, 3, 0); //Set to conversation partner's height

	// Use this for initialization
	void Start ()
    {
        transform.localPosition += Offset; //Text is created at NPC's position. Offset brings it above their head
        
        //in convo: go.GetComponent<TextMesh>().text = whatever string you want.
    } 
	
}
