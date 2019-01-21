using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public ArchiveReceiver archiveReceiver;
    public LightBoard lightBoard;

    private VRBasics_Hinge leverHinge;
    private bool solved = false;

    // Interface to set spring of lever on/off
    public void SetSpring(bool on)
    {
        leverHinge.useSpringToMax = on;
    }

    private void SolvePuzzle()
    {
        lightBoard.TurnOn();
        archiveReceiver.Reveal();

        // Makes the lever auto bounce back to the "solved" position
        leverHinge.useSpringToMin = true;

        solved = true;
    }

    private void Start()
    {
        leverHinge = GetComponentInChildren<VRBasics_Hinge>();
    }

    void Update ()
    {
        if (!solved)
        {
            bool withinSolveRange = leverHinge.percentage <= .1;

            if (!leverHinge.useSpringToMax && withinSolveRange)
            {
                SolvePuzzle();
            }
        }
    }
}
