using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour {


    public Dictionary<string, WallButton> movableWalls;

    public WallButton[] walls;

    

	// Use this for initialization
	void Start () {
        //fill the dictionary
        movableWalls = new Dictionary<string, WallButton>();
        movableWalls.Clear();
        foreach(WallButton mw in walls)
        {

            movableWalls.Add(mw.pianoKey, mw);

        }
	}

    

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("PianoCylinder"))
        //{


        //    insertCylinder(other.transform.root.gameObject.GetComponent<PianoCylinder>());
            
        //}
    }

    // Update is called once per frame
    void Update () {
		
	}

    void insertCylinder(PianoCylinder inserted)
    {
       
        //construct our string
        string s = "";

        //s += inserted.color.ToString();

        PianoCylinder[] allChildren = inserted.GetComponentsInChildren<PianoCylinder>();

       
        
        foreach(PianoCylinder p in allChildren)
        {

            s += p.color.ToString();

        }


        //check if it's the key to any walls
        if (movableWalls.ContainsKey(s))
        {

            movableWalls[s].Move();

        }

    }
}
