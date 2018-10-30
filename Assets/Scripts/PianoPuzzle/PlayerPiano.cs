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
        if(other.CompareTag("PunchPaper"))
        {


            insertPaper(other.gameObject.GetComponent<PlayerPianoPaper>());
            other.transform.localScale *= 0.001f;
            other.transform.parent = gameObject.transform;

        }
    }

    // Update is called once per frame
    void Update () {
		
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

            movableWalls[s].Move();

        }

    }
}
