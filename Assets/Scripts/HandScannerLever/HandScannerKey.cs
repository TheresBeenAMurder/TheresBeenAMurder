using UnityEngine;

public class HandScannerKey : Key
{
    public Lever lever;

    private bool solved;

    public override Puzzle PuzzleType()
    {
        return Puzzle.HandScanner;
    }

    public override void Solve()
    {
        if (!solved)
        {
            // Allow the lever to be moved
            lever.SetSpring(false);
            solved = true;
        }
    }
}
