using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramFader : MonoBehaviour
{

    public AudioSource originalHum;
    public AudioSource glitchHum;

    private float blend = 0;

    public Material hologramMat;

    public GameObject leftHand;
    public GameObject rightHand;

    public float tolerance = 15f;
    public float scale;
    public float maxAlpha = 4;

    float soundScale;

    private void Start()
    {
        scale = maxAlpha / tolerance;
        soundScale = .5f/ tolerance;
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
            blend = closestDist * soundScale;
        }
        else
        {
            blend = 1;
        }
        setAudio();
    }

    void setAudio()
    {
        originalHum.volume = (blend * .7f);
        glitchHum.volume = (1- blend) * .7f;
    }
}
