using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHand : MonoBehaviour {

    public GameObject parentHand;

    public float minY;
    public float maxY;

    public float minX;
    public float maxX;

    public float multiplyFactor = 1.2f;

    private float prevY;
    private float prevX;

   private  float xDiff;
    private float yDiff;

    public bool isVertical = true;
    public bool isTransition = false;

	// Use this for initialization
	void Start () {
        prevY = parentHand.transform.position.y;
        prevX = parentHand.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        float currentY = parentHand.transform.position.y;
        float currentX = parentHand.transform.position.x;

        xDiff = currentX - prevX;
        yDiff = currentY - prevY;

        prevX = currentX;
        prevY = currentY;


        if (!isVertical || isTransition)
        {
            if (transform.position.x + xDiff * multiplyFactor <= maxX && transform.position.x + xDiff * multiplyFactor >= minX)
            {
                transform.position += new Vector3(transform.position.x + xDiff * multiplyFactor, transform.position.y, transform.position.z);
            }
            else if (transform.position.x + xDiff * multiplyFactor > maxX)
            {
                transform.position += new Vector3(maxX, transform.position.y, transform.position.z);
            }
            else if (transform.position.x + xDiff * multiplyFactor < minX)
            {
                transform.position += new Vector3(minX, transform.position.y, transform.position.z);
            }

        }
        if(isVertical || isTransition)
        {

            if (transform.position.y + yDiff * multiplyFactor <= maxY && transform.position.y + yDiff * multiplyFactor >= minY)
            {
                transform.position += new Vector3(transform.position.x, transform.position.y + yDiff *multiplyFactor, transform.position.z);
            }
            else if (transform.position.y + yDiff * multiplyFactor > maxY)
            {
                transform.position += new Vector3(transform.position.x, maxY, transform.position.z);
            }
            else if (transform.position.y + xDiff * multiplyFactor < minX)
            {
                transform.position += new Vector3(transform.position.x, minY, transform.position.z);
            }

        }


		
	}

    public void DetermineDirection()
    {

        //determine if the xDiff or yDiff is greater
        if (xDiff > yDiff)
        {
            isVertical = false;
        }
        else
        {

        }

    }
}
