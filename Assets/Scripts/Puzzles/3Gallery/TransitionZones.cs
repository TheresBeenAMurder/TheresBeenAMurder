using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZones : MonoBehaviour {

    public float midHeight;
    public float midWidth;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("GhostHand"))
        {
            GhostHand gh = other.GetComponent<GhostHand>();
            gh.SwapDirection();
            bool isLeft = true;

            if(other.name.Contains("Right"))
            {
                isLeft = false;
            }

            if (isLeft)
            {
                if (gh.isVertical)
                {
                    //is it on the left or the right
                    if (gh.transform.localPosition.x < 0) //it's on the left
                    {
                        gh.transform.localRotation = Quaternion.Euler(0,0,0);
                    }
                    else //it's on the right
                    {
                        gh.transform.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                }
                else
                {
                    //is it on the top or bottom
                    if (gh.transform.localPosition.y < 0) //it's on the bottom
                    {
                        gh.transform.localRotation = Quaternion.Euler(0,0,90);
                    }
                    else //it's on the top
                    {
                        gh.transform.localRotation = Quaternion.Euler(0, 0, 270);
                    }
                }

            }
            else
            {
                if (gh.isVertical)
                {
                    //is it on the left or the right
                    if (gh.transform.localPosition.x < 0) //it's on the left
                    {
                        gh.transform.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                    else //it's on the right
                    {
                        gh.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else
                {
                    //is it on the top or bottom
                    if (gh.transform.localPosition.y < 0) //it's on the bottom
                    {
                        gh.transform.localRotation = Quaternion.Euler(0, 0, 270);
                    }
                    else //it's on the top
                    {
                        gh.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    }
                }

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GhostHand"))
        {
            

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
