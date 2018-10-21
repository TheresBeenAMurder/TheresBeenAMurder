using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour {


    public Dictionary<string, MovableWall> movableWalls;

    public MovableWall[] walls;

	// Use this for initialization
	void Start () {
        //fill the dictionary
        movableWalls.Clear();
        foreach(MovableWall mw in walls)
        {

            movableWalls.Add(mw.pianoKey, mw);

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
