using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMenu : ScrollRect, IMoveHandler
{
    public GameObject leftHand;
    public GameObject rightHand;
    public float ySpeed = 0f;

    private bool isTouching = false;
    private Vector3 oldPos;
    private GameObject touchingHand;

    public void OnMove(AxisEventData eventData)
    {
        ySpeed += eventData.moveVector.y * (Mathf.Abs(ySpeed) + .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHand")
        {
            isTouching = true;
            touchingHand = other.gameObject;
            oldPos = new Vector3(0, touchingHand.transform.position.y, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerHand")
        {
            isTouching = false;
            touchingHand = null;
        }
    }

    private void Update()
    {
        if (isTouching)
        {
            if (Gestures.IsPointing(leftHand, rightHand).name == touchingHand.name)
            {
                ySpeed = oldPos.y - touchingHand.transform.position.y;
                float vPos = verticalNormalizedPosition + (ySpeed * scrollSensitivity);

                ySpeed = Mathf.Lerp(ySpeed, 0, .1f);
                normalizedPosition = new Vector2(horizontalNormalizedPosition, vPos);
            }
        }
    }
}
