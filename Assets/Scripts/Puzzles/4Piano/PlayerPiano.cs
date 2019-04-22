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

    public IEnumerator playPianoSounds()
    {
        piano.clip = insertRoll;
        piano.Play();
        Debug.Log("Playing clicks");

        yield return new WaitForSeconds(insertRoll.length);

        piano.clip = pianoSound;
        piano.Play();
        Debug.Log("Playing music");
    }

    public void InsertCylinder()
    {
        cover.SetActive(true);

        // Makes sure that Mavis' conversation is only accessible once,
        // even if the walls move multiple times
        if (!wallsMoved)
        {
            wallToMove.Move();
            StartCoroutine(playPianoSounds());
            playerConversation.CanAccuse();
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
}
