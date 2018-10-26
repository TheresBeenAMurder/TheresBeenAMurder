using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject murderBoardBase;
    public OVRInput.Button murderBoardButton;
    public GameObject playerHead;

	private void Start ()
    {
        // make sure that the player can't see this upon start
        murderBoardBase.GetComponent<Renderer>().enabled = false;
        murderBoardBase.SetActive(false);
	}

    private void Update()
    {
        if (OVRInput.GetDown(murderBoardButton))
        {
            if (murderBoardBase.activeSelf)
            {
                // turn it off
                murderBoardBase.GetComponent<Renderer>().enabled = false;
                murderBoardBase.SetActive(false);
            }
            else
            {
                // turn it on
                murderBoardBase.SetActive(true);
                Vector3 newPos = new Vector3(playerHead.transform.position.x,
                    playerHead.transform.position.y,
                    playerHead.transform.position.z + .75f);
                murderBoardBase.GetComponent<Rigidbody>().MovePosition(newPos);
                murderBoardBase.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
