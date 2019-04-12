using System.Collections.Generic;
using UnityEngine;

public class PlayerPiano : MonoBehaviour
{
    public Transform leftmost;
    public NPC madeline;
    public MavisConversation mavisConvo;
    public WallButton wallToMove;
    public Transform snapPoint;
    public VictorConversation victorConvo;
    public AudioSource player;

    public GameObject cover;

    private bool wallsMoved = false;

    void snapCylinder(GameObject insertedCyl)
    {
    
        

    }

   
    //public IEnumerator<WaitForSeconds> ShowCover()
    //{

       
    //   // yield return new WaitForSeconds(1);
    //   // cover.SetActive(false);


    //}


    public void InsertCylinder()
    {
        cover.SetActive(true);
        wallToMove.Move();
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoCartridge"))            
        {
            Destroy(other.gameObject);
            InsertCylinder();
            
        }
    }

    public void Start ()
    {
	}
}
