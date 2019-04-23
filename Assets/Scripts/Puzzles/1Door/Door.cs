using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource detectiveAudio;
    public AudioClip detectiveClip;
    public InvitationSpawner invitationSpawner;
    public NPC madeline;
    public AudioSource madelineAudio;
    public AudioClip madelineHint1;
    public AudioClip madelineHint2;
    public AudioClip madelineRoom;
    public AudioClip madelineSolve;
    public Transform moveSpace;
    public float moveTime = 1 / .1f;
    public PlantWall bioPuzzle;
    public TeleportTargetHandlerPhysical teleportAllowance;
    public AudioSource victorAudio;
    public AudioClip victorClip;
    
    private bool isSolved = false;

    public IEnumerator Hint()
    {
        // Wait for 2 min after opening dialogue to play hints
        yield return new WaitForSeconds(60);

        if (!isSolved)
        {
            // Play Madeline's voiceline
            madelineAudio.clip = madelineHint1;
            madelineAudio.Play();

            yield return new WaitForSeconds(madelineHint1.length + .3f);

            if (!isSolved)
            {
                // Play Detective's response
                detectiveAudio.clip = detectiveClip;
                detectiveAudio.Play();
            }

            yield return new WaitForSeconds(detectiveClip.length + .3f);

            if (!isSolved)
            {
                // Play Madeline's response
                madelineAudio.clip = madelineHint2;
                madelineAudio.Play();
            }
        }
    }

    public bool IsSolved()
    {
        return isSolved;
    }

	public IEnumerator Open()
    {
        isSolved = true;

        madeline.AddAvailableConversation(60);

        // Kill the opening dialogue if the player solves the door puzzle while they're talking.
        invitationSpawner.StopOpeningDialogue();

        // Switch the teleportation layer to only allow teleportation inside the room
        // once the door puzzle is solved.
        teleportAllowance.AimCollisionLayerMask = LayerMask.GetMask("Floor");

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Start the timer for the bio wall hint
        StartCoroutine(bioPuzzle.Hint());

        yield return StartCoroutine(Movement.SmoothMove(moveSpace.position, moveTime, rigidbody));

        // Madeline says "finally" as door opens
        madelineAudio.clip = madelineSolve;
        madelineAudio.Play();

        yield return new WaitForSeconds(madelineSolve.length + 2);

        // Victor says "hmm" 
        victorAudio.clip = victorClip;
        victorAudio.Play();

        yield return new WaitForSeconds(victorClip.length + 2);

        // Madeline says "look at what changed"
        madelineAudio.clip = madelineRoom;
        madelineAudio.Play();
    }
}
