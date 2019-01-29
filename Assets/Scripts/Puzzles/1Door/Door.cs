using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform moveSpace;
    public float moveTime = 1 / .1f;
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
        //Vector3 newPos = new Vector3(transform.localPosition.x + 1.75f, transform.localPosition.y, transform.localPosition.z);
        yield return StartCoroutine(Movement.SmoothMove(moveSpace.position, moveTime, rigidbody));
    }
}
