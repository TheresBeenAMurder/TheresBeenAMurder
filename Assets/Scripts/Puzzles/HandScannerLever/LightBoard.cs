using UnityEngine;

public class LightBoard : MonoBehaviour
{
    public GameObject[] lights;
    public Material lightsOn;

    public AudioSource powerUpSound;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        powerUpSound.Play();
        foreach (GameObject light in lights)
        {
            light.GetComponent<Renderer>().material = lightsOn;
        }
    }
}
