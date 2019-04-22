using UnityEngine;
using System.Collections;

public class PlayerPiano : MonoBehaviour
{
    public Transform leftmost;
    public NPC madeline;
    public MavisConversation mavisConvo;
    public WallButton wallToMove;
    public Transform snapPoint;
    public VictorConversation victorConvo;
    public AudioSource piano;
    public PlayerConversation playerConversation;

    public AudioClip insertRoll;
    public AudioClip pianoSound;

    public GameObject cover;

    private bool wallsMoved = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            InsertCylinder();
        }
    }

    public void InsertCylinder()
    {
        cover.SetActive(true);

        // Makes sure that Mavis' conversation is only accessible once,
        // even if the walls move multiple times
        if (!wallsMoved)
        {
            wallToMove.Move();
            //StartCoroutine(playPianoSounds());
            playerConversation.CanAccuse();
            StartCoroutine(mavisConvo.AfterWalls());
            StartCoroutine(victorConvo.AfterWalls());
            wallsMoved = true;
            piano.clip = insertRoll;
            piano.Play();

            while (piano.isPlaying)
            { }

            piano.clip = pianoSound;
            piano.Play();
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
}
