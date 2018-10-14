using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class puzzleMaster : MonoBehaviour {

    public VRTK_SnapDropZone red;
    public VRTK_SnapDropZone blue;
    public VRTK_SnapDropZone green;

    public GameObject redSnapped;
    public GameObject blueSnapped;
    public GameObject greenSnapped;

    public GameObject redCapsule;
    public GameObject blueSphere;
    public GameObject greenCube;

    public bool win = false;

    public GameObject winObj;

	// Use this for initialization
	void Start () {
        red.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(runCheck);
        blue.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(runCheck);
        green.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(runCheck);

        winObj.SetActive(false);
         
    }
	
    public void runCheck(object o, SnapDropZoneEventArgs e)
    {
       

        bool done = true;


        //check red
        if (red.GetCurrentSnappedObject() == null)
        {
            done = false;

        }

        else if(red.GetCurrentSnappedObject().name != redCapsule.name)
        {
            done = false;

        }

        //check green

        if(done)
        {
            if(green.GetCurrentSnappedObject() == null)
            {
                done = false;

            }
            else if (green.GetCurrentSnappedObject().name != greenCube.name)
            {
                done = false;

            }
        }

        //chack blue

        if (done)
        {
            if(blue.GetCurrentSnappedObject() == null)
            {
                done = false;

            }
            else if (blue.GetCurrentSnappedObject().name != blueSphere.name)
            {
                done = false;

            }
        }

        

        if(done && !win)
        {
            winPuzzle();
        }

    }

    void winPuzzle()
    {
        win = true;
        winObj.SetActive(true);
        Debug.Log("Win");
    }
}
