using System.Collections;
using UnityEngine;

public static class Movement
{
    private static float CalculateRemainingDistance(Vector3 end, Rigidbody rigidbody)
    {
        return (rigidbody.transform.position - end).sqrMagnitude;
    }
   
    // Smoothly moves an object from its current position to the end position in moveTime
    public static IEnumerator SmoothMove(Vector3 end, float moveTime, Rigidbody rigidbody)
    {
        float sqrRemainingDistance = CalculateRemainingDistance(end, rigidbody);

        while (sqrRemainingDistance > .0000000001f)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, end, moveTime * Time.deltaTime);

            rigidbody.MovePosition(newPosition);

            sqrRemainingDistance = CalculateRemainingDistance(end, rigidbody);

            yield return null;
        }
    }

    public static IEnumerator SmoothRotate(Quaternion end, float moveTime, Rigidbody rigidbody)
    {

        while (rigidbody.transform.rotation != end)
        {
            Quaternion newPosition = Quaternion.RotateTowards(rigidbody.rotation, end, 5);

            rigidbody.transform.rotation = newPosition;
        }

            yield return null;
    }

    public static IEnumerator SmoothRotate(Vector3 end, float angle, float moveTime, Rigidbody rigidbody)
    {
        Quaternion from = rigidbody.transform.rotation;
        Quaternion to = rigidbody.transform.rotation;
        to *= Quaternion.Euler(end * angle);

        float elapsed = 0.0f;
        while (elapsed < moveTime)
        {
            rigidbody.transform.rotation = Quaternion.Slerp(from, to, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rigidbody.transform.rotation = to;
    }
}
