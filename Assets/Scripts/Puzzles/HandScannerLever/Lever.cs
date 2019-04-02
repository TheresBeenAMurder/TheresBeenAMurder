using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Light[] archiveLights;
    public ArchiveReceiver archiveReceiver;
    public LightBoard lightBoard;

    private VRBasics_Hinge leverHinge;
    private bool solved = false;

    // Interface to set spring of lever on/off
    public void SetSpring(bool on)
    {
       // leverHinge.useSpringToMin = on;
    }

    private void SolvePuzzle()
    {
        lightBoard.TurnOn();

        // Makes the lever auto bounce back to the "solved" position
        leverHinge.useSpringToMax = true;

        // Turn on the archive lights
        foreach (Light light in archiveLights)
        {
            light.gameObject.SetActive(true);
        }

        solved = true;
    }

    private void Start()
    {
        leverHinge = GetComponentInChildren<VRBasics_Hinge>();

        // Turn off the archive lights at the start of the game
        foreach (Light light in archiveLights)
        {
            light.gameObject.SetActive(false);
        }
    }

    void Update ()
    {
        if (!solved)
        {
            bool withinSolveRange = leverHinge.percentage >= .9;

            if (!leverHinge.useSpringToMin && withinSolveRange)
            {
                SolvePuzzle();
            }
        }
    }
}
