using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject murderBoardBase;
    public OVRInput.Button murderBoardButton;
    public GameObject playerHead;

    private ScrollMenu murderBoard;

    public GameObject polaroid;
    public OVRInput.Button polaroidToggleButton;

  //  public TakePicture camera;
    //public OVRInput.Button takePolaroidButton;


    private IEnumerator CreateMurderBoard()
    {
        murderBoardBase.SetActive(true);

        // set up the new position of the murder board
        Vector3 newPos = new Vector3(playerHead.transform.position.x,
            playerHead.transform.position.y,
            playerHead.transform.position.z + .75f);
        murderBoardBase.GetComponent<Rigidbody>().MovePosition(newPos);

        // load in the images
        murderBoard.LoadImages();

        // let it do its thing before you display it to the player
        yield return new WaitForEndOfFrame();

        // display it to the player
        murderBoardBase.GetComponent<Renderer>().enabled = true;
    }

    private void togglePolaroid()
    {
        if (polaroid.activeSelf)
        {
            // turn it off

            polaroid.SetActive(false);
        }
        else
        {
            // turn it on
            polaroid.SetActive(true);
        }



    }

	private void Start ()
    {
        // make sure that the player can't see this upon start
        murderBoardBase.GetComponent<Renderer>().enabled = false;

        // find the scroll menu and store it
        murderBoard = murderBoardBase.transform.Find("ImageSelectorCanvas")
            .Find("Scroll View").gameObject.GetComponent<ScrollMenu>();

        murderBoardBase.SetActive(false);
	}

    private void Update()
    {
        ////if (OVRInput.GetDown(murderBoardButton))
        ////{
        ////    if (murderBoardBase.activeSelf)
        ////    {
        ////        // turn it off
        ////        murderBoardBase.GetComponent<Renderer>().enabled = false;
        ////        murderBoard.KillCurrentImages();
        ////        murderBoardBase.SetActive(false);
        ////    }
        ////    else
        ////    {
        ////        // turn it on
        ////        StartCoroutine("CreateMurderBoard");
        ////    }
        ////}

        if(OVRInput.GetDown(polaroidToggleButton))
        {

            togglePolaroid();

        }


        //if (OVRInput.GetDown(takePolaroidButton))
        //{

        //    camera.takePic();

        //}
    }
}
