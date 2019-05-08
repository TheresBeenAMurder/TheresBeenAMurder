using UnityEngine;

public class HandScannerKey : Key
{
    private bool solved;

    public override Puzzle PuzzleType()
    {
        return Puzzle.HandScanner;
    }

    public override void Solve()
    {
        if (!solved)
        {
            solved = true;
        }
    }
}
