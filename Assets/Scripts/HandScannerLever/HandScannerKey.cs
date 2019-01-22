using UnityEngine;

public class HandScannerKey : Key
{
    public Lever lever;
    public LightBoard lightBoard;

    private bool solved;

    public override Puzzle PuzzleType()
    {
        return Puzzle.HandScanner;
    }

    public override void Solve()
    {
        if (!solved)
        {
            lightBoard.TurnOn();

            // Allow the lever to be moved
            lever.SetSpring(false);
            solved = true;
        }
    }
}
