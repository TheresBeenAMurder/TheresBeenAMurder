using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour {


    public Dictionary<string, WallButton> movableWalls;

    public WallButton[] walls;

    public Transform leftmost;
    bool cylinderInserted = false;

    float cooldown = 1;
    float cooldownTimer = 0;

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
        if (other.CompareTag("PianoCylinder") && !cylinderInserted)
        {


            insertCylinder(other.gameObject.GetComponent<PianoCylinder>());

        }
    }

    // Update is called once per frame
    void Update () {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if(cooldownTimer <= 0 && cylinderInserted)
        {
            cylinderInserted = false;

        }
	}

    void insertCylinder(PianoCylinder inserted)
    {
        cylinderInserted = true;
        //construct our string
        string s = "";

        //s += inserted.color.ToString();

        PianoCylinder[] allChildren = inserted.GetComponentsInChildren<PianoCylinder>();

        //now we gotta sort em
        List<PianoCylinder> childCyls = new List<PianoCylinder>();

        foreach(PianoCylinder pc in allChildren)
        {
            childCyls.Add(pc);
        }

        int newIndex = 0;

        PianoCylinder[] sorted = new PianoCylinder[allChildren.Length];

        while(childCyls.Count > 0)
        {
           
            PianoCylinder pcLeast = childCyls[0];
            foreach(PianoCylinder pc in childCyls)
            {

                if(Vector3.Distance(pc.transform.position, leftmost.transform.position) < Vector3.Distance(pcLeast.transform.position, leftmost.transform.position))
                {
                    pcLeast = pc;
                }

            }
            
            sorted[newIndex] = pcLeast;

            childCyls.Remove(pcLeast);
            newIndex++;



        }




        
        foreach(PianoCylinder p in sorted)
        {

            s += p.color.ToString();

        }


        //check if it's the key to any walls
        if (movableWalls.ContainsKey(s))
        {
            movableWalls[s].Move();
            NPC m = GameObject.FindObjectOfType<NPC>();
            m.UpdateNextPrompt(6);
        }

        cooldownTimer = cooldown;

    }
}
