using UnityEngine;

public class PuzzleSolution : MonoBehaviour
{
    private bool isSolved = false;
    private float moveTime = 1f / .1f;

    public bool IsSolved()
    {
        return isSolved;
    }

	public void PuzzleSolve()
    {
        gameObject.SetActive(false);

        isSolved = true;
    }
}
