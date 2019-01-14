using UnityEngine;

public class PlantPot : MonoBehaviour
{
    public bool containsLabel = false;
    public GameObject correctLabel;
    public PlantWall plantHolder;

    private GameObject currentLabel;

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
            plantHolder.CheckForSolution();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject == currentLabel)
        {
            currentLabel = null;
            containsLabel = false;
        }
    }

    private void Start()
    {
        plantHolder = transform.parent.gameObject.GetComponent<PlantWall>();
    }
}
