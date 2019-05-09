using UnityEngine;

public class FallingObjectNet : MonoBehaviour
{
    public Door doorPuzzle;
    public CharacterController player;
    public TeleportTransitionBlink blink;

    private Vector3 postDoorSpawnPos = new Vector3(-0.16f, 1.964f, -1.963f);
    private Vector3 preDoorSpawnPos = new Vector3(3.73f, 1.964f, 12.082f);

    public void OnTriggerEnter(Collider other)
    {
        Vector3 newPosition = preDoorSpawnPos;
        if (doorPuzzle.IsSolved())
        {
            newPosition = postDoorSpawnPos;
        }

        if (other.CompareTag("Player"))
        {
            StartCoroutine(blink.BlinkCoroutineForce(newPosition, player));
        }
        else
        {
            other.transform.position = newPosition;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}
