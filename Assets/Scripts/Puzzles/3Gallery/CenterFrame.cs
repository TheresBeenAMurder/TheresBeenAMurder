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

    private float time;
    private float timeLeft;

    private bool isPaused;

    private bool on;

	// Use this for initialization
	void Start () {
        StartCoroutine("Vibrate");
        on = false;
        

	}
	
    void Vibrate()
    {
        while(true)
        {
            if(!isPaused)
            {
                timeLeft -= Time.deltaTime;
                if(timeLeft <= 0)
                {
                    if (on)
                    {
                        //turn off
                    }
                    else
                    {
                        //turn on
                    }

                    timeLeft = time;
                }
            }
        }

    }

    
    

	// Update is called once per frame
	void Update () {
        bool inRange = false;
        inRange = ((Vector3.Distance(transform.position, leftHand.transform.position) <= range) || (Vector3.Distance(transform.position, rightHand.transform.position) <= range));


        if (isPaused && inRange)
        {
            isPaused = false;

        }
        else if (!isPaused && !inRange)
        {
            isPaused = true;
        }
        else if (!isPaused && inRange)
        {
            //calculate the distance & set time accordingly
            //should we base it on the closest hand or the eaverage of the two? we're going to stick with the closest hand for now n can change it later
            float sendTime = (Vector3.Distance(transform.position, leftHand.transform.position));
            if(Vector3.Distance(transform.position, rightHand.transform.position) < sendTime)
            {
                sendTime = (Vector3.Distance(transform.position, rightHand.transform.position));
            }

            //calculate the percentage, then multiply by the difference between highest and lowest frequency, then add to the lowest frequency

            sendTime = range / sendTime;

            float difference = highFreq - lowFreq;

            sendTime *= difference;
            sendTime += lowFreq;

            sendTime = 1 / sendTime;

            time = sendTime;

        }

	}
    
}
