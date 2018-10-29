using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderBoard : MonoBehaviour {

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.tag == "BoardPhoto")
        {
            Debug.Log("A photo is touching me");
        }
    }
}
