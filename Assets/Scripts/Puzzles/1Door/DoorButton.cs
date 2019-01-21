using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public int id;
    public Material pressedMaterial;

    private Material defaultMaterial;
    private bool isPressed;
    private ButtonPanel panel;

    public Text feedback;

    public void OnTriggerEnter(Collider other)
    {
        if (!isPressed && !panel.LoggingChoice() && other.gameObject.tag == "Player")
        {
            StartCoroutine("PressButton");
        }
    }

    // Makes button unable to be pressed again for 1 second after it is
    // initially pressed.
    public IEnumerator PressButton()
    {
        Renderer renderer = GetComponent<Renderer>();
        feedback.text += id.ToString();
        // "Press" the button
        isPressed = true;
        renderer.material = pressedMaterial;
        yield return (StartCoroutine(panel.LogChoice(id)));
        

        // button is no longer pressed
        isPressed = false;
        renderer.material = defaultMaterial;
    }

    public void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        panel = transform.parent.gameObject.GetComponent<ButtonPanel>();
    }
}
