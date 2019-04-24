using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    Light testLight;

    // Start is called before the first frame update
    void Start()
    {
        testLight = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.001f, 2f));
            testLight.enabled = !testLight.enabled;
        }
    }
}
