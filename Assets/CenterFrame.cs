using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour {

    public GameObject image;

    public float lowFreq;
    public float highFreq;

    public float range;

    public GameObject leftHand;
    public GameObject rightHand;



	// Use this for initialization
	void Start () {
		
	}
	
    /// <summary>
    /// Vibrates a controller
    /// </summary>
    /// <param name="controller">Controller to vibrate. 0 for left, 1 for right</param>
    /// <param name="distancePercentage">Percentage of the total range.</param>
    void vibrate(int controller, float distancePercentage)
    {
        while(true)
        {



        }

    }

	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, leftHand.transform.position) <= range)
        {


        }


	}

    void pulseNearby()
    {



    }
}
