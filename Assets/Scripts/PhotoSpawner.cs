using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSpawner : MonoBehaviour {

    public VerticalLayoutGroup _photoLayoutGroup;

    private GameObject[] _photos;

    GameObject activeHand = null;

    GameObject[] _handsColliding;

    public GameObject _rightHand;
    public GameObject _leftHand;

    public GameObject _photoPrefab;

    public bool isGrabbingPhoto = false;
    

	// Use this for initialization
	void Start () {
        _handsColliding = new GameObject[2]; // left is 0, right is 1
        _handsColliding[0] = null;
        _handsColliding[1] = null;

        refreshLayoutGroup();
	}

    private void OnTriggerEnter(Collider other)
    {

        //active hand is set to whatever hand most recently entered the collider
        if (other.gameObject.name == _leftHand.name)
        {
            _handsColliding[0] = other.gameObject;
            activeHand = other.gameObject;

           
        }
       else if (other.gameObject.name == _rightHand.name)
        {
            _handsColliding[1] = other.gameObject;
            activeHand = other.gameObject;
            
        }


    }

    private void OnTriggerExit(Collider other)
    {

        //if the other hand is still in there, active hand switches to that hand. Otherwise, it's null.
        if (other.gameObject.name == _leftHand.name)
        {
            _handsColliding[0] = null;
            if (_handsColliding[1] != null)
            {
            
                activeHand = _handsColliding[1];
  

            }
            else
            {
                activeHand = null;
                
            }
        }
        else if (other.gameObject.name == _rightHand.name)
        {
            _handsColliding[1] = null;
            if (_handsColliding[0] != null)
            {
                activeHand = _handsColliding[0];
            }
            else
            {
                activeHand = null;
                
            }
        }

    }

    public void refreshLayoutGroup()
    {

        _photos = new GameObject[_photoLayoutGroup.transform.childCount];

        int i = 0;
        foreach(Transform child in _photoLayoutGroup.transform)
        {

            _photos[i] = child.gameObject;
            i++;
        }


    }

    private void checkForPhotoGrab()
    {
        if(activeHand != null) //first we gotta know if there's a hand in here.
        {
            if(Gestures.IsGrabbing(_leftHand, _rightHand))
            {
                GameObject closestPicture = null;
               
               
                foreach(GameObject photo in _photos)
                {


                    float distance = Vector3.Distance(photo.transform.position, activeHand.transform.position);
                   

                    if (closestPicture != null)
                    {
                        if(Vector3.Distance(photo.transform.position, activeHand.transform.position) < Vector3.Distance(closestPicture.transform.position, activeHand.transform.position)) // if this one is closer than the last one, update closestPicture to it.
                        {
                            closestPicture = photo;


                        }

                    }
                    else
                    {
                        closestPicture = photo;

                    }


                    

                } // find the closest picture

                isGrabbingPhoto = true;
                spawnPhoto(closestPicture);

               // Debug.Log(closestPicture.name);
            }


        }
        else if(!Gestures.IsGrabbing(_leftHand, _rightHand))
        {

            isGrabbingPhoto = false;

        }
    }

    void spawnPhoto(GameObject photoToSpawn)
    {


        GameObject photo = Instantiate(_photoPrefab, activeHand.transform);
        Canvas photoCanv = photo.GetComponentInChildren<Canvas>();
        RawImage photoImg = photoCanv.GetComponentInChildren<RawImage>();
        Texture photoGrabbed = photoToSpawn.GetComponent<RawImage>().texture;

       // Debug.Log(photoGrabbed.name);

        photoImg.texture = photoGrabbed;

        //isGrabbingPhoto = false;
    }


    // Update is called once per frame
    void Update () {
		if(!isGrabbingPhoto)
        {

            checkForPhotoGrab();

        }
        else if(Gestures.IsGrabbing(_leftHand, _rightHand) == null)
        {

            isGrabbingPhoto = false;

        }
	}
}
