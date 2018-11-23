using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float moveTime = 1 / .1f;
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

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 newPos = new Vector3(transform.position.x,
            transform.position.y,
            transform.parent.position.z - newZPos);
        yield return StartCoroutine(Movement.SmoothMove(newPos, moveTime, rigidbody));

        // Switch the teleportation layer to only allow teleportation inside the room
        // once the door puzzle is solved.
        teleportAllowance.AimCollisionLayerMask = LayerMask.GetMask("Floor");
    }
}
