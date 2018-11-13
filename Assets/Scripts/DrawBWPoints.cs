using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBWPoints : MonoBehaviour {
	private LineRenderer lineRenderer; 
	//private float counter; 
	//private float distance; 

	//public float drawSpeed = 5f;

    public Transform[] pointsToDraw;



	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
        pointsToDraw = new Transform[2];
		
	}
	

    public void addPhoto(Transform photo)
    {

        


    }


	// Update is called once per frame
	void Update () {
		
		lineRenderer.SetPosition(1, pointsToDraw[1].position);
	}
}
