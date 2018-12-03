using System.Collections;
using UnityEngine;

public class PuzzleSolution : MonoBehaviour {

    public NPC madeline;

    private bool isSolved = false;
    private float moveTime = 1f / .1f;

    public bool IsSolved()
    {
        return isSolved;
    }

	public void PuzzleSolve()
    {
        //StartCoroutine("RevealPainting");
        gameObject.SetActive(false);

        madeline.UpdateNextPrompt(12);
        madeline.canAccuse = true;
        isSolved = true;

        // "Find" the motive for Madeline when the plant puzzle is solved
        madeline.gameObject.GetComponent<Accusation>().FindEvidence("motive", madeline.id);
    }

    // Moves painting cover so painting is now visible
    public void RevealPainting()
    {
        //////Rigidbody rigidbody = GetComponent<Rigidbody>();

        //////// Slide cover behind painting
        //////Vector3 newPos = new Vector3(transform.position.x,
        //////    transform.position.y,
        //////    transform.parent.position.z + 2);

        //////yield return StartCoroutine(Movement.SmoothMove(newPos, moveTime, rigidbody));
    }
}
