using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramFader : MonoBehaviour
{

    public Material hologramMat;

    public GameObject leftHand;
    public GameObject rightHand;

    public float tolerance = 5f;
    public float scale;
    public float maxAlpha = 4;

    private void Start()
    {
        scale = maxAlpha / tolerance;
    }

    // Update is called once per frame
    void Update()
    {
        //see which hand is closer

        GameObject closestHand = leftHand;
        if(Vector3.Distance(rightHand.transform.position, transform.position) < Vector3.Distance(leftHand.transform.position, transform.position))
        {
            closestHand = rightHand;
        }

        float closestDist = Vector3.Distance(closestHand.transform.position, transform.position);

        if (closestDist < tolerance)
        {

            hologramMat.SetFloat("_OverallAlpha", closestDist * scale);

        }
    }
}
