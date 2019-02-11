using System.Linq;
using UnityEngine;

// Generic Trigger Sensor class, use in combo with Key.cs
public class Sensor : MonoBehaviour
{
    public Material correctColor;
    public int[] correctKeyIDs;
    public Material incorrectColor;
    public Puzzle puzzle;

    private Key currentKey;
    private Material originalColor;

    private void ChangeColor(Material newColor)
    {
        gameObject.GetComponent<Renderer>().material = newColor;
    }

    public void OnTriggerExit(Collider other)
    {
        Key key = other.gameObject.GetComponent<Key>();

        // Only change color when key exits, nothing else
        if (key != null && key == currentKey)
        {
            ChangeColor(originalColor);
            currentKey = null;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Key key = other.gameObject.GetComponent<Key>();
        if (key != null && currentKey == null)
        {
            currentKey = key;
            if (key.PuzzleType() == puzzle && correctKeyIDs.Contains(key.ID))
            {
                //  PUZZLE SOLVED HERE
                ChangeColor(correctColor);
                key.Solve();
            }
            else
            {
                ChangeColor(incorrectColor);
            }
        }
    }

    public void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material;
    }
}
