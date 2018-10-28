using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMenu : ScrollRect, IMoveHandler
{
    public GameObject leftHand;
    public GameObject rightHand;
    public float ySpeed = .5f;

    private bool isTouching = false;
    private Vector3 oldPos;
    private GameObject touchingHand;

    public void KillCurrentImages()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void LoadImages()
    {
        // Automatically populate the scroll view with player's pictures
        string fileDir = Application.dataPath + "/Resources/MurderBoardPictures";
        string[] files = Directory.GetFiles(fileDir, "*.jpg");
        GameObject[] images = new GameObject[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            // create new GameObject and make it a child of content
            images[i] = new GameObject("image" + i);

            // add a RawImage to the object and set its texture to the photo
            images[i].AddComponent<RawImage>();
            Texture photo = Resources.Load<Texture>("MurderBoardPictures/" + Path.GetFileNameWithoutExtension(files[i]));
            images[i].GetComponent<RawImage>().texture = photo;

            images[i].transform.parent = base.content.transform;
            images[i].GetComponent<RectTransform>().localScale = new Vector3(2f, .7f, 1f);
            images[i].transform.localPosition = new Vector3(0, 0, 0);
        }
    }

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
        if (isTouching && touchingHand != null)
        {
            GameObject pointingHand = Gestures.IsPointing(leftHand, rightHand);
            if (pointingHand != null && pointingHand.name == touchingHand.name)
            {
                ySpeed = oldPos.y - touchingHand.transform.position.y;
                float vPos = verticalNormalizedPosition + (ySpeed * scrollSensitivity);

                ySpeed = Mathf.Lerp(ySpeed, 0, .1f);
                normalizedPosition = new Vector2(horizontalNormalizedPosition, vPos);
            }
        }
    }
}
