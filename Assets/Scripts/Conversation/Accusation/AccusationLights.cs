using UnityEngine;

public class AccusationLights : MonoBehaviour
{
    public GameObject lights;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (lights.activeSelf)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
    }

    public void TurnOn()
    {
        lights.SetActive(true);
    }

    public void TurnOff()
    {
        lights.SetActive(false);
    }
}
