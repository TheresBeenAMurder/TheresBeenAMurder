using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour {

    public Material defaultMaterial;
    public Material onPressMaterial;
    // walls that this button can move, please add walls
    // top-down (index 0 is top wall, last index is bottom wall)
    public List<Wall> walls;

    private bool buttonPressed;
    private bool playerNear;
    private bool goingUp;

	// Use this for initialization
	void Start ()
    {
        buttonPressed = false;
        playerNear = false;
        goingUp = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (playerNear && OVRInput.GetDown(OVRInput.Button.One) && !buttonPressed)
        {
            GetComponent<MeshRenderer>().material = onPressMaterial;
            StartCoroutine(MoveWalls());
            buttonPressed = true;
        }
    }

    private IEnumerator MoveWalls()
    {
        // need to set walls to active to move them
        foreach (Wall wall in walls)
        {
            wall.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(.5f);

        // goes up until it reaches the last wall, then goes down until
        // it reaches the last wall, rinse, repeat
        if (goingUp)
        {
            if (walls[walls.Capacity - 1].CanMoveUp())
            {
                yield return walls[walls.Capacity - 1].MoveUp();
            }
            else
            {
                yield return walls[0].MoveDown();
                goingUp = false;
            }
        }
        else
        {
            if (walls[0].CanMoveDown())
            {
                yield return walls[0].MoveDown();
            }
            else
            {
                yield return walls[walls.Capacity - 1].MoveUp();
                goingUp = true;
            }
        }

        // shows the movement smoothly before disabling wall
        yield return new WaitForSeconds(.5f);

        // disables the walls we don't see
        foreach (Wall wall in walls)
        {
            wall.UpdateVisibility();
        }

        buttonPressed = false;
        GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNear = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNear = false;
        }
    }
}
