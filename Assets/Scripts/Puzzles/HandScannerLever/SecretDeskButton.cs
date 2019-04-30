using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDeskButton : MonoBehaviour {

    public bool solved = false;

    public Combiner archive;
    public Light[] archiveLights;
    public LightBoard lightBoard;
    public Extractor extractor;

   
    public void SolvePuzzle()
    {
        lightBoard.TurnOn();
        extractor.powerOn();

        Debug.Log("solving");
        // Turn on the archive lights
        foreach (Light light in archiveLights)
        {
            light.gameObject.SetActive(true);
        }

        archive.isActive = true;
        solved = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
