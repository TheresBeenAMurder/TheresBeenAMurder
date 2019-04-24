using UnityEngine;
using System.Collections;

public class PlayerPiano : MonoBehaviour
{
    public Transform leftmost;
    public NPC madeline;
    public MavisConversation mavisConvo;
    public AudioSource playerAudio;
    public WallButton wallToMove;
    public Transform snapPoint;
    public NPC victor;
    public AudioSource victorAudio;
    public AudioClip victorHint;
    public VictorConversation victorConvo;
    public AudioSource piano;
    public PlayerConversation playerConversation;

    public AudioClip insertRoll;
    public AudioClip pianoSound;

    public GameObject cover;

    private bool wallsMoved = false;

    public IEnumerator Hint()
    {
        // Wait for 1 min after the gallery is revealed to play hint
        yield return new WaitForSeconds(60);

        if (!wallsMoved)
        {
            while (playerConversation.inConversation || playerAudio.isPlaying)
            {
                // prevents updating victor's voiceline while the player
                // is actively in a conversation with him
                yield return new WaitForSeconds(10);
            }

            // Play Victor's voiceline
            victorAudio.clip = victorHint;
            victorAudio.Play();
            victor.AddAvailableConversation(66);
        }
    }

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

        yield return new WaitForSeconds(insertRoll.length);

        piano.clip = pianoSound;
        piano.Play();
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

            victor.RemoveAvailableConversation(66);
            victor.RemoveAvailableConversation(64);
            madeline.RemoveAvailableConversation(68);

            victor.AddAvailableConversation(74);
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
