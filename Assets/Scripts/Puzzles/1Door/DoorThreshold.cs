using UnityEngine;

public class DoorThreshold : MonoBehaviour
{
    public Door door;

    private bool madelineIn = false;
    private bool mavisIn = false;
    private bool playerIn = false;
    private bool victorIn = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Madeline"))
        {
            madelineIn = true;
        }
        else if (other.CompareTag("Mavis"))
        {
            mavisIn = true;
        }
        else if (other.CompareTag("Player"))
        {
            playerIn = true;
        }
        else if (other.CompareTag("Victor"))
        {
            victorIn = true;
        }

        if (EveryoneInRoom())
        {
            door.everyoneInRoom = true;
        }
    }

    private bool EveryoneInRoom()
    {
        return (madelineIn && mavisIn && playerIn && victorIn);
    }
}
