using System.Collections;
using UnityEngine;

public class PuzzleSolution : MonoBehaviour {

    public float moveTime = 1f / .1f;

    private bool isSolved = false;

    public bool IsSolved()
    {
        return isSolved;
    }

	public void PuzzleSolve()
    {
        StartCoroutine("RevealPainting");
        isSolved = true;
    }

    // Moves painting cover so painting is now visible
    public IEnumerator RevealPainting()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Slide cover to left of painting
        Vector3 newPos = new Vector3(transform.parent.position.x + 1,
            transform.position.y,
            transform.position.z);

        yield return StartCoroutine(Movement.SmoothMove(newPos, moveTime, rigidbody));
    }
}
