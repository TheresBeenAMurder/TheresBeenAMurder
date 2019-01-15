using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour
{
    public Transform leftmost;
    public Dictionary<string, WallButton> movableWalls;
    public WallButton[] walls;
    public Transform snapPoint;

    void snapCylinder(GameObject insertedCyl)
    {

        insertedCyl.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
        insertedCyl.transform.parent = transform;
        insertedCyl.transform.rotation = Quaternion.Euler(0, transform.rotation.y, 90);

    }

    public void InsertCylinder(PianoCylinder inserted)
    {
        

        //construct our string
        string s = "";

        PianoCylinder[] allChildren = inserted.GetComponentsInChildren<PianoCylinder>();

        //now we gotta sort em
        List<PianoCylinder> childCyls = new List<PianoCylinder>();

        foreach (PianoCylinder pc in allChildren)
        {
            childCyls.Add(pc);
        }

        int newIndex = 0;

        PianoCylinder[] sorted = new PianoCylinder[allChildren.Length];

        while (childCyls.Count > 0)
        {
            PianoCylinder pcLeast = childCyls[0];
            foreach (PianoCylinder pc in childCyls)
            {
                if (Vector3.Distance(pc.transform.position, leftmost.transform.position) < Vector3.Distance(pcLeast.transform.position, leftmost.transform.position))
                {
                    pcLeast = pc;
                }
            }

            sorted[newIndex] = pcLeast;

            childCyls.Remove(pcLeast);
            newIndex++;
        }

        foreach (PianoCylinder p in sorted)
        {
            s += p.color.ToString();
        }

        snapCylinder(inserted.gameObject);

        //check if it's the key to any walls
        if (movableWalls.ContainsKey(s))
        {
            movableWalls[s].Move();
            NPC m = GameObject.FindObjectOfType<NPC>();
            m.UpdateNextPrompt(6);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //only check the top one in the hierarchy. i'm brilliant
        if (other.CompareTag("PianoCylinder") &&
            (other.transform.parent == null || !other.transform.parent.CompareTag("PianoCylinder")))
        {
            InsertCylinder(other.gameObject.GetComponent<PianoCylinder>());
        }
    }

    public void Start ()
    {
        //fill the dictionary
        movableWalls = new Dictionary<string, WallButton>();
        movableWalls.Clear();
        foreach(WallButton mw in walls)
        {
            movableWalls.Add(mw.pianoKey, mw);
        }
	}
}
