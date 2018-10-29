using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBWPoints : MonoBehaviour {
	private LineRenderer lineRenderer; 
	private float counter; 
	private float distance; 

	public float drawSpeed = 5f; 
	public Transform pointA; 
	public Transform pointB; 
	public Transform pointC; 
	public Transform pointD;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, pointA.position);
		
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(1, pointB.position);
		lineRenderer.SetPosition(2, pointC.position);
		lineRenderer.SetPosition(3, pointD.position);
		//lineRenderer.SetPosition(1, pointB.position);
	}
}
