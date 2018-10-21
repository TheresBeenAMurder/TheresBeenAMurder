using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour {


    public Dictionary<string, MovableWall> movableWalls;

    public MovableWall[] walls;

    public PlayerPianoPaper test;

	// Use this for initialization
	void Start () {
        //fill the dictionary
        movableWalls = new Dictionary<string, MovableWall>();
        movableWalls.Clear();
        foreach(MovableWall mw in walls)
        {

            movableWalls.Add(mw.pianoKey, mw);

        }
        
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            insertPaper(test);

        }
	}

    void insertPaper(PlayerPianoPaper inserted)
    {
       
        //construct our string
        string s = "";

        for(int i = 0; i < inserted._nodeStatus.Length; i++)
        {
            if (inserted._nodeStatus[i])
            {
                s += "1";

            }
            else
            {
                s += "0";

            }


        }
        //check if it's the key to any walls
        if (movableWalls.ContainsKey(s))
        {

            movableWalls[s].moveWall();

        }

    }
}
