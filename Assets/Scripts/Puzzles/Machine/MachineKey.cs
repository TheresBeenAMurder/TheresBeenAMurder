public class MachineKey : Key
{
    public override Puzzle PuzzleType()
    {
        return Puzzle.Machine;
    }

    public override void Solve()
    {
        Destroy(gameObject);
    }
}
