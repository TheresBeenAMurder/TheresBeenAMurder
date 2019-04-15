using UnityEngine;

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

    public GameObject cover;

    private bool wallsMoved = false;

    public void InsertCylinder()
    {
        cover.SetActive(true);

        // Makes sure that Mavis' conversation is only accessible once,
        // even if the walls move multiple times
        if (!wallsMoved)
        {
            wallToMove.Move();
            piano.Play();
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
