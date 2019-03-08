using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    public float lowFreq;
    public float highFreq;

    public GameObject leftGhost;
    public GameObject rightGhost;

    public float ghostRange;
    public float vibrateRange;

    public GameObject leftHand;
    public GameObject rightHand;

    private float time;
    private float timeLeft;

    [HideInInspector]
    public bool isPaused = true;

    private GameObject disabledHand;
    private bool ghostOn = false;
    private GameObject grabbingHand;
    private bool hapticsOn = false;
    private bool isGrabbing = false;

    public OVRHapticsClip hapticsClip;

    private void Start ()
    {
        HapticSetup();
	}

    private void HapticSetup()
    {
        hapticsClip = new OVRHapticsClip(10);
        for (int i = 0; i < 10; i++)
        {
            hapticsClip.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)255;
        }
        hapticsClip = new OVRHapticsClip(hapticsClip.Samples, hapticsClip.Samples.Length);
    }

    // Determines if a player's hands are within the given range of the frame
    private bool InRange(float range)
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
            inRange = ((Vector3.Distance(transform.position, leftHand.transform.position) <= range) ||
                (Vector3.Distance(transform.position, rightHand.transform.position) <= range));
        }

        return inRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enabled && other.CompareTag("Player") && !ghostOn && isGrabbing)
        {
            TurnGhostHandsOn();
        }
    }

    private void SetHaptics()
    {
        //calculate the distance between player hands and frame & set time accordingly
        //should we base it on the closest hand or the average of the two? we're going to stick with the closest hand for now n can change it later
        float sendTime = Mathf.Min(Vector3.Distance(transform.position, leftHand.transform.position),
            Vector3.Distance(transform.position, rightHand.transform.position));

        //calculate the percentage, then multiply by the difference between highest and lowest frequency, then add to the lowest frequency
        sendTime = vibrateRange / sendTime;

        float difference = highFreq - lowFreq;
        sendTime *= difference;
        sendTime += lowFreq;

        sendTime = 1 / sendTime;
        time = sendTime;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (hapticsOn)
            {
                //turn off
                OVRHaptics.LeftChannel.Clear();
                OVRHaptics.RightChannel.Clear();
            }
            else
            {
                //turn on
                OVRHaptics.LeftChannel.Preempt(hapticsClip);
                OVRHaptics.RightChannel.Preempt(hapticsClip);
            }

            timeLeft = time;
        }
    }

    public void TurnGhostHandsOff()
    {
        isPaused = true;
        leftGhost.SetActive(false);
        rightGhost.SetActive(false);
        ghostOn = false;

        // Turn normal hand on
        if (disabledHand != null)
        {
            disabledHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            disabledHand = null;
        }
    }

    private void TurnGhostHandsOn()
    {
        if (grabbingHand != null)
        {
            if (grabbingHand == rightHand)
            {
                rightGhost.SetActive(true);
            }
            else
            {
                leftGhost.SetActive(true);
            }

            isPaused = false;
            ghostOn = true;

            // Turn normal hand off
            grabbingHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            disabledHand = grabbingHand;
        }
    }

    // Disabling object turns off updates - will only update if it's enabled
    private void Update()
    {
        // Updates whether the player is making a grabbing motion or not & with which hand
        grabbingHand = Gestures.IsGrabbing(leftHand, rightHand);
        if (grabbingHand == null)
        {
            isGrabbing = false;
        }
        else
        {
            isGrabbing = true;
        }

        bool inGhostRange = InRange(ghostRange);
        if (!ghostOn)
        {
            if (inGhostRange)
            {
                TurnGhostHandsOn();
            }
        }
        else
        {
            if (!isGrabbing || !inGhostRange)
            {
                TurnGhostHandsOff();
            }
        }

        // Player should be getting haptic feedback as they get closer
        if (InRange(vibrateRange))
        {
            SetHaptics();
        }
    }
}
