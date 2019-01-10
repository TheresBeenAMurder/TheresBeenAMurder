using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Material correctColor;
    public int correctKeyID;
    public Material incorrectColor;

    private Key currentKey;
    private Material originalColor;

    private void ChangeColor(Material newColor)
    {
        gameObject.GetComponent<Renderer>().material = newColor;
    }

    public void OnCollisionExit(Collision collision)
    {
        Key key = collision.collider.gameObject.GetComponent<Key>();

        // Only change color when key exits, nothing else
        if (key != null && key == currentKey)
        {
            ChangeColor(originalColor);
            currentKey = null;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        Key key = collision.collider.gameObject.GetComponent<Key>();
        if (key != null && currentKey == null)
        {
            currentKey = key;
            if (key.ID == correctKeyID)
            {
                //  PUZZLE SOLVED HERE, DO THE OTHER STUFF
                ChangeColor(correctColor);
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
