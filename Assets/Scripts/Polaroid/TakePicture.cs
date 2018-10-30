using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour {
	public Camera imageCaptureCamera;
	
	public Texture2D photo; 
	public MeshRenderer photoModel;

	// Use this for initialization
	void Start () {
		photo = new Texture2D(imageCaptureCamera.targetTexture.width, imageCaptureCamera.targetTexture.height);
		
	}
	
	// Update is called once per frame
	void Update () {

        if(OVRInput.GetDown(OVRInput.Button.Three))
        {

            takePic();

        }
		//if(Input.GetKeyDown(KeyCode.Space))
  //      {
  //          takePic();

  //      }
	} 

    void takePic()
    {
        photo = WriteRTImage(imageCaptureCamera);
        photoModel.material.mainTexture = photo;
        

    }

	 Texture2D WriteRTImage(Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
}
