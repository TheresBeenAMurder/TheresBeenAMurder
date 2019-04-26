using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AutoConversation autoConversation1;
    public AutoConversation autoConversation2;
    public AutoConversation firstIdleConvo;
    public ConversationUpdater conversationUpdater;
    public IdleConversation idleConversation;
    public InvitationSpawner invitationSpawner;

    public Transform moveSpace;
    public float moveTime = 1 / .1f;
    public PlantWall bioPuzzle;
    public TeleportTargetHandlerPhysical teleportAllowance;
    
    private bool isSolved = false;

    public IEnumerator Hint()
    {
        // Wait for 2 min after opening dialogue to play hints
        yield return new WaitForSeconds(60);

        if (!isSolved)
        {
            yield return autoConversation1.PlayDialogue();
        }
    }

    public bool IsSolved()
    {
        return isSolved;
    }

	public IEnumerator Open()
    {
        isSolved = true;

        conversationUpdater.OpenConversation(1);

        // Kill the opening dialogue if the player solves the door puzzle while they're talking.
        invitationSpawner.StopOpeningDialogue();

        // Switch the teleportation layer to only allow teleportation inside the room
        // once the door puzzle is solved.
        teleportAllowance.AimCollisionLayerMask = LayerMask.GetMask("Floor");

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Start the timer for the bio wall hint
        StartCoroutine(bioPuzzle.Hint());
        // Start the idle conversation coroutine
        StartCoroutine(idleConversation.PlayIdleConversations());

        yield return StartCoroutine(Movement.SmoothMove(moveSpace.position, moveTime, rigidbody));

        yield return autoConversation2.PlayDialogue();

        yield return new WaitForSeconds(30);

        yield return firstIdleConvo.PlayDialogue();
    }
}
