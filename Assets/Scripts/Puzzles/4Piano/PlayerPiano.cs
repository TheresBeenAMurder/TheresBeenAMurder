using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour
{
    public Transform leftmost;
    public NPC madeline;
    public MavisConversation mavisConvo;
    public Dictionary<string, WallButton> movableWalls;
    public WallButton[] walls;
    public Transform snapPoint;
    public VictorConversation victorConvo;
    public AudioSource player;

    public GameObject cover;

    private bool wallsMoved = false;

    void snapCylinder(GameObject insertedCyl)
    {
        insertedCyl.transform.position = snapPoint.position;

        //rotate either 0 or 180

        if (insertedCyl.transform.localRotation.x > 0 && insertedCyl.transform.rotation.x < 180)
        {
            insertedCyl.transform.rotation = new Quaternion(90, 0, 0, 0);
        }
        else
        {
            insertedCyl.transform.rotation = new Quaternion(-90, 0, 0, 0);
        }
        

    }

   
    public IEnumerator<WaitForSeconds> ShowCover()
    {

        cover.SetActive(true);
        yield return new WaitForSeconds(1);
        cover.SetActive(false);


    }


    public void InsertCylinder(PianoCartridge inserted)
    {

        CartridgeDisc[] allChildren = inserted.discs;

        StartCoroutine(ShowCover());
       // snapCylinder(inserted.gameObject);
        //construct our string
        string s = "";

        //now we gotta sort em
        List<CartridgeDisc> childCyls = new List<CartridgeDisc>();

        foreach (CartridgeDisc pc in allChildren)
        {
            childCyls.Add(pc);
        }

        int newIndex = 0;

        CartridgeDisc[] sorted = new CartridgeDisc[allChildren.Length];

        while (childCyls.Count > 0)
        {
            CartridgeDisc pcLeast = childCyls[0];
            foreach (CartridgeDisc pc in childCyls)
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

        foreach (CartridgeDisc p in sorted)
        {
            s += p.ID.ToString();
        }

        snapCylinder(inserted.gameObject);

        //check if it's the key to any walls
        if (movableWalls.ContainsKey(s))
        {
            movableWalls[s].Move();
            madeline.UpdateNextPrompt(-1);

            // Makes sure that Mavis' conversation is only accessible once,
            // even if the walls move multiple times
            if (!wallsMoved)
            {
                player.Play();
                StartCoroutine(mavisConvo.AfterWalls());
                StartCoroutine(victorConvo.AfterWalls());
                wallsMoved = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //only check the top one in the hierarchy. i'm brilliant
        if (other.CompareTag("PianoCartridge"))            
        {
            InsertCylinder(other.gameObject.GetComponent<PianoCartridge>());
            
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
