using UnityEngine;

public class LightBoard : MonoBehaviour
{
    public GameObject[] lights;
    public Material lightsOn;

    public void TurnOn()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Renderer>().material = lightsOn;
        }
    }
}
