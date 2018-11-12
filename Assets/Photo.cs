using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour {


    public LineDrawHandler parent;

    public DrawBWPoints currentLine;



	// Use this for initialization
	void Start () {
        parent = GameObject.FindObjectOfType<LineDrawHandler>();
        currentLine = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
