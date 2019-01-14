using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float moveTime = 1 / .1f;
    public Transform newPosTransform;
    public float newZPos = .295f;
    public TeleportTargetHandlerPhysical teleportAllowance;
    
    private bool isSolved = false;

    public bool IsSolved()
    {
        return isSolved;
    }

	public IEnumerator Open()
    {
        isSolved = true;

        // Switch the teleportation layer to only allow teleportation inside the room
        // once the door puzzle is solved.
        teleportAllowance.AimCollisionLayerMask = LayerMask.GetMask("Floor");

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 newPos = newPosTransform.position;
        yield return StartCoroutine(Movement.SmoothMove(newPos, moveTime, rigidbody));
    }
}
