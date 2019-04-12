using System.Collections;
using UnityEngine;

// Takes 4 objects and combines them to create a key for
// the archives.
public class Combiner : MonoBehaviour
{
    public Creator creator;
    public Material pressedMaterial;
    public MachineSensor[] sensors = new MachineSensor[4];

    private MachineKey[] currentKeys = new MachineKey[4];
    private Material defaultMaterial;
    private bool isPressed = false;

    public Rigidbody extractorDoor;
    public Transform doorOpenPos;
    public Transform doorClosePos;

    // Returns true if all sensors are populated, false otherwise
    // If true, currentKeys contains the keys on the sensors
    public bool CheckKeys()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!sensors[i].ContainsKey())
            {
                return false;
            }

            currentKeys[i] = sensors[i].currentKey;
        }

        return true;
    }

    // Returns a string comprised of 4 characters
    private string CombineKeys()
    {
        string combined = "";

        foreach (MachineKey key in currentKeys)
        {
            combined += key.ID;
        }

        return combined;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.gameObject.tag == "Player")
        {
            StartCoroutine("PressButton");
        }
    }

    // Makes button unable to be pressed again for 3 seconds after it is
    // initially pressed. (We can make this the animation time)
    public IEnumerator PressButton()
    {
        StartCoroutine(Movement.SmoothMove(doorClosePos.position, 1.5f, extractorDoor));
        Renderer renderer = GetComponent<Renderer>();

        // "Press" the button
        isPressed = true;
        renderer.material = pressedMaterial;


        // Wait for animation
        yield return new WaitForSeconds(4);

        if (CheckKeys())
        {
            creator.CreateKey(CombineKeys());
        }


        // button is no longer pressed
        isPressed = false;
        renderer.material = defaultMaterial;

        StartCoroutine(Movement.SmoothMove(doorOpenPos.position, 2f, extractorDoor));
    }

    private void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }
}