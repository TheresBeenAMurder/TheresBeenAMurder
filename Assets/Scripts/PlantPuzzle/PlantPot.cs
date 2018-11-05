using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPot : MonoBehaviour {

    public GameObject correctLabel;
    public GameObject labelHolder;

    private bool containsLabel = false;
    private GameObject currentLabel;
    private Vector3 originalScale;
    private PlantTable plantHolder;

    public bool IsCorrect()
    {
        return (currentLabel != null && correctLabel == currentLabel);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!containsLabel && other.gameObject.tag == "PlantLabel")
        {
            currentLabel = other.gameObject;
            originalScale = currentLabel.transform.localScale;

            currentLabel.transform.parent = labelHolder.transform;
            currentLabel.transform.localScale = new Vector3(1, 1, 1);
            Rigidbody labelRigidbody = currentLabel.GetComponent<Rigidbody>();
            labelRigidbody.MovePosition(new Vector3(0, 0, 0));

            plantHolder.CheckForSolution();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject == currentLabel)
        {
            currentLabel.transform.parent = null;
            currentLabel.transform.localScale = originalScale;
            currentLabel = null;
        }
    }

    private void Start()
    {
        plantHolder = transform.parent.gameObject.GetComponent<PlantTable>();
        originalScale = transform.localScale;
    }
}
