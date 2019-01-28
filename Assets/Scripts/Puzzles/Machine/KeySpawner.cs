using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject prefab;
    public Material spawnColor;
    public GameObject table;

    private bool isPressed = false;
    private Material originalColor;

    public void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            PressButton();
        }
    }

    public void PressButton()
    {
        Renderer renderer = GetComponent<Renderer>();

        // "Press" the button
        isPressed = true;
        renderer.material = spawnColor;
        SpawnObjects();

        // button is no longer pressed
        isPressed = false;
        renderer.material = originalColor;
    }

    private void SpawnObjects()
    {
        GameObject keys = Instantiate(prefab, table.transform);
        keys.transform.localPosition += new Vector3(-3, 3, 0);
    }
}
