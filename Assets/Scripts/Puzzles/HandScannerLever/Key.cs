using UnityEngine;

public abstract class Key : MonoBehaviour
{
    public int ID;

    public abstract Puzzle PuzzleType();

    public abstract void Solve();
}

// Add additional puzzles here as needed
// Needed so that multiple puzzles can use the same IDs
public enum Puzzle { HandScanner, Archive, Machine };