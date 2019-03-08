﻿using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public NPC madeline;
    public NPC mavis;
    public AudioSource mavisAudio;
    public AudioClip mavisHint;
    public Transform moveSpace;
    public float moveTime = 1 / .1f;
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
            mavisAudio.clip = mavisHint;
            mavisAudio.Play();

            // Unlock the conversation with Mavis
            mavis.UpdateNextPrompt(18);
        }
    }

    public bool IsSolved()
    {
        return isSolved;
    }

	public IEnumerator Open()
    {
        isSolved = true;

        // Remove the unlocked conversation with Mavis
        mavis.UpdateNextPrompt(-1);

        // Unlock Madeline's piano conversation
        madeline.UpdateNextPrompt(21);
        madeline.AddAvailableConversation(25);

        // Switch the teleportation layer to only allow teleportation inside the room
        // once the door puzzle is solved.
        teleportAllowance.AimCollisionLayerMask = LayerMask.GetMask("Floor");

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Start the timer for the bio wall hint
        StartCoroutine(bioPuzzle.Hint());

        //Vector3 newPos = new Vector3(transform.localPosition.x + 1.75f, transform.localPosition.y, transform.localPosition.z);
        yield return StartCoroutine(Movement.SmoothMove(moveSpace.position, moveTime, rigidbody));
    }
}
