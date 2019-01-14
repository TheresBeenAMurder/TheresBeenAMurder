using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isCurrentWall = false;
    public string pianoKey;
    public Wall wallAbove;
    public Wall wallBelow;

    private float moveTime = 1f / .1f;

    public bool CanMoveDown()
    {
        return (!isCurrentWall || wallAbove != null);
    }

    public bool CanMoveUp()
    {
        return (!isCurrentWall || wallBelow != null);
    }

    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }

    // call from the highest wall in the stack
    public IEnumerator MoveDown()
    {
        float wallHeight = transform.position.y - wallBelow.transform.position.y;
        SetCurrentWall(false);

        Rigidbody parentRigidbody = GetComponentInParent<Rigidbody>();
        Vector3 end = new Vector3(parentRigidbody.position.x,
            parentRigidbody.position.y - wallHeight,
            parentRigidbody.position.z);

        yield return StartCoroutine(Movement.SmoothMove(end, moveTime, parentRigidbody));
    }

    // call from the lowest wall in the stack
    public IEnumerator MoveUp()
    {
        float wallHeight = wallAbove.transform.position.y - transform.position.y;
        SetCurrentWall(true);

        Rigidbody parentRigidbody = GetComponentInParent<Rigidbody>();
        Vector3 end = new Vector3(parentRigidbody.position.x,
            parentRigidbody.position.y + wallHeight,
            parentRigidbody.position.z);

        yield return StartCoroutine(Movement.SmoothMove(end, moveTime, parentRigidbody));
    }

    // goes through the walls and figures out which is the new current wall
    private void SetCurrentWall(bool moveUp)
    {
        if (moveUp)
        {
            if (wallAbove == null)
            {
                return;
            }

            if (wallAbove.isCurrentWall)
            {
                isCurrentWall = true;
                wallAbove.isCurrentWall = false;
                return;
            }

            wallAbove.SetCurrentWall(true);
        }
        else
        {
            if (wallBelow == null)
            {
                return;
            }

            if (wallBelow.isCurrentWall)
            {
                isCurrentWall = true;
                wallBelow.isCurrentWall = false;
                return;
            }

            wallBelow.SetCurrentWall(false);
        }
    }

    public void Start()
    {
        UpdateVisibility();
    }

    // only sets the current wall to be visible to the player bc rendering
    public void UpdateVisibility()
    {
        gameObject.SetActive(isCurrentWall);
    }
}
