using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource openingDialogue;
    public GameObject outsideFloor;

    private int outsideFloorLayer;

    public void Start()
    {
        outsideFloorLayer = outsideFloor.layer;

        // Switch the outside floor layer so you can't teleport
        outsideFloor.layer = 0;
    }

    public void Update()
    {
        if (!openingDialogue.isPlaying)
        {
            outsideFloor.layer = outsideFloorLayer;
            enabled = false;
        }
    }
}
