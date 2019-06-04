using UnityEngine;

public class PlantPot : MonoBehaviour
{
    public bool containsLabel = false;
    public GameObject correctLabel;
    public PlantWall plantHolder;

    private GameObject currentLabel;

    public Transform snapPoint;

    public bool IsCorrect()
    {
        return (currentLabel != null && correctLabel == currentLabel);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!containsLabel && other.gameObject.tag == "PlantLabel")
        {
            currentLabel = other.gameObject;
            containsLabel = true;
            LabelGrabbable label = other.gameObject.GetComponent<LabelGrabbable>();
     
            if(label != null)
            { 
                label.snapTransform = snapPoint;
                label.isInDropZone = true;
            }

            plantHolder.CheckForSolution();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject == currentLabel)
        {
            LabelGrabbable label = other.gameObject.GetComponent<LabelGrabbable>();
            if (label != null)
            {
                label.isInDropZone = false;
            }
            currentLabel = null;
            containsLabel = false;
        }
    }

    private void Start()
    {
        plantHolder = transform.parent.gameObject.GetComponent<PlantWall>();
    }
}
