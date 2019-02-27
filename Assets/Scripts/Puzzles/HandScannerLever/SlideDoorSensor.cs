using UnityEngine;

// Necessary because the rail percentages aren't firing consistently
public class SlideDoorSensor : MonoBehaviour
{
    public GameObject doorCollider;
    public ArchiveReceiver receiver;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == doorCollider)
        {
            receiver.Solve();
        }
    }
}
