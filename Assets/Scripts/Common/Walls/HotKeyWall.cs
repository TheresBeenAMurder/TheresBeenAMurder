using UnityEngine;

public class HotKeyWall : MonoBehaviour
{
    public WallButton wallToMove;
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.W))
        {
            wallToMove.Move();
        }
	}
}
