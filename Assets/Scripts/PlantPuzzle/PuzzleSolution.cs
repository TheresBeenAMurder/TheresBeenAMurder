using System.Collections;
using UnityEngine;

public class PuzzleSolution : MonoBehaviour {

    public Madeline madeline;

    private bool isSolved = false;
    private float moveTime = 1f / .1f;

    public bool IsSolved()
    {
        return isSolved;
    }

	public void PuzzleSolve()
    {
        StartCoroutine("RevealPainting");
        madeline.UpdateNextPrompt(12);
        isSolved = true;
    }

    // Moves painting cover so painting is now visible
    public IEnumerator RevealPainting()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Slide cover behind painting
        Vector3 newPos = new Vector3(transform.position.x,
            transform.position.y,
            transform.parent.position.z + 2);

        yield return StartCoroutine(Movement.SmoothMove(newPos, moveTime, rigidbody));
    }
}
