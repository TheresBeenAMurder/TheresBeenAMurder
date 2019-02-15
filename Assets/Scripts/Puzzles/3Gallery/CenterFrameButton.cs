using System.Collections;
using UnityEngine;

public class CenterFrameButton : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    public CenterFrame parent;
    private bool done = false;

    public GameObject indicator;
    public float range = 0.3f;

    public CenterFramePivot pivot;

    // Hint related
    public AudioSource playerAudio;
    public PlayerConversation playerConversation;
    public NPC victor;
    public AudioSource victorAudio;
    public AudioClip victorHint;

    private void Update()
    {
        //if (!done)
        //{
        //    if (Vector3.Distance(leftHand.transform.position, transform.position) <= range)
        //    {
        //        if (Gestures.IsGrabbing(leftHand, rightHand) != null && Gestures.IsGrabbing(leftHand, rightHand).name == leftHand.name)
        //        {

        //            buttonPressed();
        //        }
        //    }
        //    if (Vector3.Distance(rightHand.transform.position, transform.position) <= range)
        //    {
        //        if (Gestures.IsGrabbing(leftHand, rightHand) != null && Gestures.IsGrabbing(leftHand, rightHand).name == rightHand.name)
        //        {

        //            buttonPressed();
        //        }
        //    }

        //}
    }

    public IEnumerator Hint()
    {
        // Wait for 1 min after the gallery is revealed to play hint
        yield return new WaitForSeconds(60);

        if (!done)
        {
            while (playerConversation.inConversation || playerAudio.isPlaying)
            {
                // prevents updating victor's voiceline while the player
                // is actively in a conversation with him
                yield return new WaitForSeconds(10);
            }

            // Play Victor's voiceline
            victorAudio.clip = victorHint;
            victorAudio.Play();

            // Unlock the hint conversation with Victor
            victor.UpdateNextPrompt(24);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!done && other.CompareTag("GhostHand"))
        {
            Debug.Log("GO");
            buttonPressed();
            parent.isPaused = true;
            //parent.end = true;
        }
        
    }


    void buttonPressed()
    {
        done = true;
        parent.Done();
        pivot.RotateOpen();
        Debug.Log("Pivoting");
        Destroy(parent.leftGhost);
        Destroy(parent.rightGhost);

        victor.UpdateNextPrompt(-1);
    }
}
