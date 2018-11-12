using Mono.Data.Sqlite;
using System.Data;
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

        // Reset current conversation to be the first one when the scene loads
        // Reset relationship value to default when scene loads
        string connection = "URI=file:" + Application.dataPath + "/Database.db";
        database = (IDbConnection)new SqliteConnection(connection);
        database.Open();
        command = database.CreateCommand();

        string query = "SELECT DefaultPromptID, DefaultRelValue FROM 'Characters' WHERE ID == " + id;
        command.CommandText = query;
        reader = command.ExecuteReader();

        reader.Read();
        int firstPromptID = reader.GetInt32(0);
        int relationshipVal = reader.GetInt32(1);
        reader.Close();

        string update = "UPDATE Characters SET PromptID = " + firstPromptID + " WHERE ID ==" + id;
        command.CommandText = update;
        command.ExecuteNonQuery();

        update = "UPDATE Characters SET RelationshipValue = " + relationshipVal + " WHERE ID ==" + id;
        command.CommandText = update;
        command.ExecuteNonQuery();

        command.Dispose();
        command = null;

        database.Close();
        database = null;
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
