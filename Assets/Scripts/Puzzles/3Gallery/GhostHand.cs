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

    public Transform startPos;

	// Use this for initialization
	void Start () {
        prevY = parentHand.transform.localPosition.y;
        prevX = parentHand.transform.localPosition.x;
        transform.rotation = new Quaternion(0, 0, 0, 0);
//        isVertical = true;
	}
	
	// Update is called once per frame
	void Update () {
        float currentY = parentHand.transform.localPosition.y;
        float currentX = parentHand.transform.localPosition.x;

        xDiff = currentX - prevX;
        yDiff = currentY - prevY;

        prevX = currentX;
        prevY = currentY;
        

        if (!isVertical)
        {
            if (transform.localPosition.x + xDiff * multiplyFactor <= maxX && transform.localPosition.x + xDiff * multiplyFactor >= minX)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + xDiff * multiplyFactor, transform.localPosition.y, transform.localPosition.z);
            }
            else if (transform.localPosition.x + xDiff * multiplyFactor > maxX)
            {
                transform.localPosition = new Vector3(maxX, transform.localPosition.y, transform.localPosition.z);
            }
            else if (transform.localPosition.x + xDiff * multiplyFactor < minX)
            {
                transform.localPosition = new Vector3(minX, transform.localPosition.y, transform.localPosition.z);
            }

        }
        if(isVertical)
        {

            if (transform.localPosition.y + yDiff * multiplyFactor <= maxY && transform.localPosition.y + yDiff * multiplyFactor >= minY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + yDiff *multiplyFactor, transform.localPosition.z);
            }
            else if (transform.localPosition.y + yDiff * multiplyFactor > maxY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
            }
            else if (transform.localPosition.y + xDiff * multiplyFactor < minX)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, minY, transform.localPosition.z);
            }

        }


		
	}

    public void SwapDirection()
    {

        isVertical = !isVertical;
        

    }
}
