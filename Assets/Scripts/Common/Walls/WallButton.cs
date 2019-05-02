using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    public string pianoKey;
    public AudioSource sfx;

    // walls that this button can move, please add walls
    // top-down (index 0 is top wall, last index is bottom wall)
    public List<Wall> walls;

    private bool goingUp = false;

    public void Move(AutoConversation autoConvo)
    {
        StartCoroutine(MoveWalls(autoConvo));
    }

    private IEnumerator MoveWalls(AutoConversation autoConvo)
    {
        sfx.Play();

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
                yield return walls[0].MoveDown(autoConvo);
                goingUp = false;
            }
        }
        else
        {
            if (walls[0].CanMoveDown())
            {
                yield return walls[0].MoveDown(autoConvo);
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
    }
}
