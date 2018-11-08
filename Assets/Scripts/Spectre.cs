using UnityEngine;
using UnityEngine.UI;

public class Spectre : NPC
{

    static int ID = 2;
    static string NAME = "Spectre";

    GameObject canvas = null;
    bool playerNear = false;

    public Spectre() : base(ID, NAME){}

	// Use this for initialization
	void Start ()
    {
        conversationAudio = GetComponent<AudioSource>();
        canvas = GameObject.Find("Canvas");
        displayBox = canvas.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        if (!inConversation && playerNear && Input.GetKey("e"))
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

        displayBox.text = "You are too far away to talk to Spectre";
        inConversation = false;
        playerNear = false;
    }

    private void OnTriggerStay(Collider other)
    { 
        if (!playerNear)
        {
            displayBox.text = "Press E to speak to Spectre";
            
            playerAudio = GameObject.Find("HandPrefab").GetComponent<AudioSource>();
            
            playerNear = true;
        }
    }
}
