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
        leverHinge.useSpringToMin = on;
    }

    private void SolvePuzzle()
    {
        lightBoard.TurnOn();

        // Makes the lever auto bounce back to the "solved" position
        leverHinge.useSpringToMax = true;

        archiveReceiver.Reveal();

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
            bool withinSolveRange = leverHinge.percentage >= .9;

            if (!leverHinge.useSpringToMin && withinSolveRange)
            {
                SolvePuzzle();
            }
        }
    }
}
