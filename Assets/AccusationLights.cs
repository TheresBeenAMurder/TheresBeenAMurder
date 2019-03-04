using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccusationLights : MonoBehaviour {

    public GameObject lights;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (lights.activeSelf)
            {
                turnOff();
            }
            else
            {
                turnRed();
            }
        }
    }

    void turnRed()
    {
        lights.SetActive(true);
    }

    void turnOff()
    {
        lights.SetActive(false);

    }
}
