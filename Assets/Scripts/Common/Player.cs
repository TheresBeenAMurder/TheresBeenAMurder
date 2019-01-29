using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerHead;
    public GameObject polaroid;
    public OVRInput.Button polaroidToggleButton;

    private void TogglePolaroid()
    {
        if (polaroid.activeSelf)
        {
            // turn it off
            polaroid.SetActive(false);
        }
        else
        {
            // turn it on
            polaroid.SetActive(true);
        }
    }

    private void Update()
    {
        if(OVRInput.GetDown(polaroidToggleButton))
        {
            TogglePolaroid();
        }
    }
}
