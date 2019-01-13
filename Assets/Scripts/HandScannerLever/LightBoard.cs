using System.Collections.Generic;
using UnityEngine;

public class LightBoard : MonoBehaviour
{ 
    public Material on;
    public Material off;

    private bool currentState = false;
    private List<GameObject> lights = new List<GameObject>();

    // Switches the current display (on->off, off->on)
    public void FlipSwitch()
    {
        if (currentState)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

	public void Start ()
    {
        foreach (Transform child in transform)
        {
            lights.Add(child.gameObject);
        }
	}
	
    // Turns lights off
    public void TurnOff()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Renderer>().material = off;
        }

        currentState = false;
    }

    // Turns lights on
	public void TurnOn()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Renderer>().material = on;
        }

        currentState = true;
    }
}
