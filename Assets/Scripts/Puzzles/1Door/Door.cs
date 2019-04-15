using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource madelineAudio;
    public AudioClip madelineHint;
    public Transform moveSpace;
    public float moveTime = 1 / .1f;
    public AudioSource openingDialogue;
    public PlantWall bioPuzzle;
    public TeleportTargetHandlerPhysical teleportAllowance;
    
    private bool isSolved = false;

    public IEnumerator Hint()
    {
        // Wait for 2 min after opening dialogue to play hints
        yield return new WaitForSeconds(120);

        if (!isSolved)
        {
            // Play Mavis' voiceline
            madelineAudio.clip = madelineHint;
            madelineAudio.Play();
        }
    }

    public bool IsSolved()
    {
        return isSolved;
    }

	public IEnumerator Open()
    {
        isSolved = true;

        // Kill the opening dialogue if the player solves the door puzzle while they're talking.
        if (openingDialogue.isPlaying)
        {
            openingDialogue.Stop();
        }

        // Switch the teleportation layer to only allow teleportation inside the room
        // once the door puzzle is solved.
        teleportAllowance.AimCollisionLayerMask = LayerMask.GetMask("Floor");

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Start the timer for the bio wall hint
        StartCoroutine(bioPuzzle.Hint());

        yield return StartCoroutine(Movement.SmoothMove(moveSpace.position, moveTime, rigidbody));
    }
}
