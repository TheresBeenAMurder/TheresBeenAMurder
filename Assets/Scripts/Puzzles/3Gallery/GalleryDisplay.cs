using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryDisplay : MonoBehaviour {

    public GameObject[] images;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            activateImages();
        }
	}

    public void activateImages()
    {

        foreach(GameObject image in images)
        {
            image.SetActive(true);
        }

    }
}
