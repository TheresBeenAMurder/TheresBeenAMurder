using System.Collections;
using UnityEngine;

public class CenterFrameButton : MonoBehaviour
{
    public CenterFrame centerFrame;
    public DatabaseHandler dbHandler;
    private bool done = false;

    public GameObject indicator;
    public float range = 0.3f;

    public float moveTime = 1 / .1f;
    public Transform openPosition;
    public Rigidbody frame;
    public AudioSource frameAudio;

    // Hint related
    public AudioSource playerAudio;
    public PlayerConversation playerConversation;
    public NPC victor;
    public AudioSource victorAudio;
    public AudioClip victorHint;

    private void ButtonPressed()
    {
        done = true;
        centerFrame.enabled = false;
        centerFrame.TurnGhostHandsOff();
        StartCoroutine(Movement.SmoothMove(openPosition.position, moveTime, frame));
        frameAudio.Play();

        // Ghost hands are no longer needed
        Destroy(centerFrame.leftGhost);
        Destroy(centerFrame.rightGhost);

        // Remove hint conversation with Victor
        victor.UpdateNextPrompt(-1);

        // Testing the updated accusation mechanic
        dbHandler.FindEvidence("motive", 2, 1);
        dbHandler.FindEvidence("motive", 2, 5);
        dbHandler.FindEvidence("opportunity", 3, 12);
        dbHandler.FindEvidence("alibi", 3, 13);
        dbHandler.FindEvidence("motive", 3, 8);
        dbHandler.FindEvidence("motive", 4, 14);
        dbHandler.FindEvidence("motive", 4, 15);
        dbHandler.FindEvidence("motive", 4, 16);
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
            ButtonPressed();
            centerFrame.isPaused = true;
        }
    }
}
