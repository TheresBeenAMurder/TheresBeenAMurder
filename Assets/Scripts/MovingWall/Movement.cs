using System.Collections;
using UnityEngine;

public static class Movement {

    private static float CalculateRemainingDistance(Vector3 end, Rigidbody rigidbody)
    {
        return (rigidbody.transform.position - end).sqrMagnitude;
    }

    // Smoothly moves an object from its current position to the end position in moveTime
    public static IEnumerator SmoothMove(Vector3 end, float moveTime, Rigidbody rigidbody)
    {
        float sqrRemainingDistance = CalculateRemainingDistance(end, rigidbody);

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, end, moveTime * Time.deltaTime);

            rigidbody.MovePosition(newPosition);

            sqrRemainingDistance = CalculateRemainingDistance(end, rigidbody);

            yield return null;
        }
    }
}
