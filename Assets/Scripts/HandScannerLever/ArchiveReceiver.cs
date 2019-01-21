using UnityEngine;

public class ArchiveReceiver : MonoBehaviour
{
    private bool visible = false;

    public void Reveal()
    {
        if (!visible)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);

            StartCoroutine(Movement.SmoothMove(newPos, 1f/.1f, gameObject.GetComponent<Rigidbody>()));

            visible = true;
        }
    }
}
