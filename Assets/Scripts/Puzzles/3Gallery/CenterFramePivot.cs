using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFramePivot : MonoBehaviour {
    
    public float rotationRate;

    public float goalRotationDegrees;

     bool started = false;
    bool done = false;

    private float startRotation;
    
    public void RotateOpen()
    {
        goalRotationDegrees *= Mathf.Deg2Rad;
        startRotation = transform.rotation.y;
        goalRotationDegrees += startRotation;
        started = true;
        rotationRate = (goalRotationDegrees - transform.rotation.y) / rotationRate;
       

    }
	
	// Update is called once per frame
	void Update () {
		if(started && !done)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + rotationRate * Time.deltaTime, transform.rotation.z, transform.rotation.w);
       
            if(transform.rotation.y >= goalRotationDegrees)
            {
                done = true;
            }


        }
	}
}
