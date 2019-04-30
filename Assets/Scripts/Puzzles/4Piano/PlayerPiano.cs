using UnityEngine;
using System.Collections;

public class PlayerPiano : MonoBehaviour
{
    public AutoConversation autoConversation;
    public ConversationUpdater conversationUpdater;
    public Transform leftmost;
    public AudioClip mavisCallOut;
    public WallButton wallToMove;
    public Transform snapPoint;
    public AudioClip victorHint;
    public AudioSource piano;
    public PlayerConversation playerConversation;

    public AudioClip insertRoll;
    public AudioClip pianoSound;

    public GameObject cover;

    private bool wallsMoved = false;

    private IEnumerator AfterWalls()
    {
        yield return new WaitForSeconds(120);
        yield return playerConversation.WaitToTalk();
        conversationUpdater.TriggerVoiceLine(ConversationUpdater.Character.Mavis, mavisCallOut);
        conversationUpdater.OpenConversation(5);
    }

    public IEnumerator Hint()
    {
        // Wait for 1 min after the gallery is revealed to play hint
        yield return new WaitForSeconds(60);

        if (!wallsMoved)
        {
            yield return playerConversation.WaitToTalk();
            conversationUpdater.TriggerVoiceLine(ConversationUpdater.Character.Victor, victorHint);
            conversationUpdater.OpenConversation(3);
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
            wallToMove.Move(autoConversation);
            StartCoroutine(playPianoSounds());
            playerConversation.AddAccusationConversations();

            conversationUpdater.CloseConversation(2, true);
            conversationUpdater.CloseConversation(3, true);
            conversationUpdater.CloseConversation(4, true);
            conversationUpdater.OpenConversation(6);
            conversationUpdater.OpenConversation(7);

            StartCoroutine(AfterWalls());

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
