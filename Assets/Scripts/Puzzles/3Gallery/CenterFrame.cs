using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour {

    public GameObject image;

    public float lowFreq;
    public float highFreq;

    public GameObject leftGhost;
    public GameObject rightGhost;
    bool ghost = false;

    public float range;

    public GameObject leftHand;
    public GameObject rightHand;

    private float time;
    private float timeLeft;

    private bool isPaused;

    private bool on;

    private bool isGrabbing = false;

    private bool end = false;

    OVRHapticsClip clipHard;

    // Use this for initialization
    void Start () {

        isPaused = true;
        on = false;
        hapticSetup();
	}

    public void Done()
    {
        end = true;
    }

    void hapticSetup()
    {

        clipHard = new OVRHapticsClip(10);
        for (int i = 0; i < 10; i++)
        {
            clipHard.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)255;
        }
        clipHard = new OVRHapticsClip(clipHard.Samples, clipHard.Samples.Length);

    }

    void grab()

    {
        isGrabbing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !ghost)
        {

            isPaused = true;
            ghost = true;
            if(other.name.Contains("Left"))
            {
                leftGhost.SetActive(true);
                leftGhost.GetComponent<MeshFilter>().mesh = GameObject.Find("hand_left_renderPart_0").GetComponent<SkinnedMeshRenderer>().sharedMesh;
                leftGhost.transform.position = new Vector3(leftGhost.transform.position.x, other.transform.position.y, leftGhost.transform.position.z);
            }
            else if (other.name.Contains("Right"))
            {
                rightGhost.SetActive(true);

                rightGhost.GetComponent<MeshFilter>().mesh = GameObject.Find("hand_right_renderPart_0").GetComponent<SkinnedMeshRenderer>().sharedMesh;
                rightGhost.transform.position = new Vector3(rightGhost.transform.position.x, other.transform.position.y, rightGhost.transform.position.z);
            }
        }

    }

    // Update is called once per frame
    void Update () {
        if (!end)
        {
            if (image.activeSelf && !isGrabbing)
            {
                bool inRange = false;

                if (!leftHand.activeSelf && rightHand.activeSelf)
                {
                    inRange = (Vector3.Distance(transform.position, rightHand.transform.position) <= range);
                }

                else if (!rightHand.activeSelf && leftHand.activeSelf)
                {
                    inRange = (Vector3.Distance(transform.position, leftHand.transform.position) <= range);
                }

                else if (rightHand.activeSelf && leftHand.activeSelf)
                {
                    inRange = ((Vector3.Distance(transform.position, leftHand.transform.position) <= range) || (Vector3.Distance(transform.position, rightHand.transform.position) <= range));
                }

                if (isPaused && inRange && !ghost)
                {
                    isPaused = false;
                    //calculate the distance & set time accordingly
                    //should we base it on the closest hand or the eaverage of the two? we're going to stick with the closest hand for now n can change it later
                    float sendTime = (Vector3.Distance(transform.position, leftHand.transform.position));
                    if (Vector3.Distance(transform.position, rightHand.transform.position) < sendTime)
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
                else if (!isPaused && !inRange)
                {
                    isPaused = true;
                }
                else if (!isPaused && inRange)
                {
                    //calculate the distance & set time accordingly
                    //should we base it on the closest hand or the eaverage of the two? we're going to stick with the closest hand for now n can change it later
                    float sendTime = (Vector3.Distance(transform.position, leftHand.transform.position));
                    if (Vector3.Distance(transform.position, rightHand.transform.position) < sendTime)
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


                if (!isPaused)
                {
                    timeLeft -= Time.deltaTime;
                    if (timeLeft <= 0)
                    {
                        if (on)
                        {
                            //turn off
                            OVRHaptics.LeftChannel.Clear();
                            OVRHaptics.RightChannel.Clear();

                        }
                        else
                        {
                            //turn on
                            OVRHaptics.LeftChannel.Preempt(clipHard);
                            OVRHaptics.RightChannel.Preempt(clipHard);
                        }

                        timeLeft = time;
                    }
                }

            }

            if (Gestures.IsGrabbing(leftHand, rightHand) != null)
            {

                if (Gestures.IsGrabbing(leftHand, rightHand).name == rightHand.name)
                {
                    //RH grab
                }
                if (Gestures.IsGrabbing(leftHand, rightHand).name == leftHand.name)
                {
                    //LH grab
                    // if
                }
                else
                {

                    isGrabbing = false;
                }


            }
            else isGrabbing = false;
        }


	}
    
}
