using UnityEngine;

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
        }
    }

    void Update()
    {
        if (activeHand != null)
        {
            currentX = activeHand.transform.localPosition.x;

            float difference = currentX - prevX;
            difference *= multiplier;

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
