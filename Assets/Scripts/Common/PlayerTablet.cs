using UnityEngine;
using System.Collections;

public class PlayerTablet : MonoBehaviour
{
    public RectTransform content;

    public float minX;
    public float maxX;

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject activeHand;

    float prevX;
    float currentX;
    public float multiplier;

    public float averageDelta = 0;
    int framesPassed = 0;

    public float momentumFalloff = 0.8f;
    public float momentumTolerance = 0.05f;

    private void OnTriggerEnter(Collider other)
    {
        if (activeHand == null)
        {
            if (other.name == "LeftHandAnchor")
            {
                activeHand = leftHand;

            }
            else if (other.name == "RightHandAnchor")
            {
                activeHand = rightHand;
            }

            if (activeHand != null)
            {
                prevX = activeHand.transform.localPosition.x;
                currentX = activeHand.transform.localPosition.x;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.name == "LeftHandAnchor" || other.name == "RightHandAnchor") && activeHand != null)
        {
            activeHand = null;
            framesPassed = 0;
            StartCoroutine(ScrollMomentum(averageDelta));
            averageDelta = 0;
        }
    }

    public IEnumerator ScrollMomentum(float avgDelta)
    {
        float tempE = momentumFalloff;



        while(tempE > momentumTolerance)
        {

            float distDelta = avgDelta * tempE;

            if (content.position.x + distDelta < maxX && content.position.x + distDelta > minX)
            {
                content.position = new Vector3(content.position.x + distDelta, content.position.y, content.position.z);
            }
            else
            {
                break;
            }

            tempE *= momentumFalloff;

        }

        yield return null;
    }

    void Update()
    {
        if (activeHand != null)
        {
            currentX = activeHand.transform.localPosition.x;

            float difference = currentX - prevX;
            difference *= multiplier;
            framesPassed += 1;
            if(averageDelta == 0)
            {
                averageDelta = difference;
            }
            else
            {
                averageDelta = (averageDelta + difference) / framesPassed;
                    }

            prevX = currentX;


            if (content.localPosition.x + difference >= minX && content.localPosition.x + difference <= maxX)
            {
                content.localPosition += new Vector3(difference, 0, 0);
            }
            else if (content.localPosition.x + difference < minX)
            {
                content.localPosition = new Vector3(minX, 0, 0);
            }
            else if (content.localPosition.x + difference > maxX)
            {
                content.localPosition = new Vector3(maxX, 0, 0);
            }
        }
    }
}
