using UnityEngine;
using UnityEngine.UI;

public class Madeline : NPC
{
    private static int ID = 2;
    private static string NAME = "Madeline";

    private GameObject canvas = null;
    private bool playerNear = false;

    public Madeline() : base(ID, NAME){}

	void Start ()
    {
        conversationAudio = GetComponent<AudioSource>();
        displayBox = GetComponentInChildren<Text>();
    }
	
	protected override void Update ()
    {
        if (!inConversation && playerNear && Input.GetKey("m"))
        {
            inConversation = true;
            StartConversation();
        }

        base.Update();
    }

    private void OnTriggerExit(Collider other)
    {
        if (reader != null)
        {
            reader.Close();
            reader = null;
        }

        if (command != null)
        {
            command.Dispose();
            command = null;
        }

        if (database != null)
        {
            database.Close();
            database = null;
        }

        displayBox.text = "You are too far away to talk to " + NAME;
        inConversation = false;
        playerNear = false;
    }

    private void OnTriggerStay(Collider other)
    { 
        if (!playerNear && other.gameObject.tag == "Player")
        {
            displayBox.text = "Press m to speak to " + NAME;
            playerNear = true;
        }
    }
}
