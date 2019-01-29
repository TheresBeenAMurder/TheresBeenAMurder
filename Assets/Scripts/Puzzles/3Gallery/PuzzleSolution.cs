using UnityEngine;

public class PuzzleSolution : MonoBehaviour
{
    public NPC madeline;

    private bool isSolved = false;
    private float moveTime = 1f / .1f;

    public bool IsSolved()
    {
        return isSolved;
    }

	public void PuzzleSolve()
    {
        gameObject.SetActive(false);

        madeline.UpdateNextPrompt(12);
        madeline.canAccuse = true;
        isSolved = true;

        // "Find" the motive for Madeline when the plant puzzle is solved
        madeline.gameObject.GetComponent<Accusation>().FindEvidence("motive", madeline.id);
    }
}
