using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

// This is a base class meant to be used for all NPC characters
public class NPC : MonoBehaviour {

    private static int RelationshipLowerBound = 0;
    private static int RelationshipUpperBound = 10;

    public enum relationshipStatus {hate, dislike, neutral, like};

    // NPC traits
    public string characterName;
    public int dislikeThreshold;
    public int id;
    public int likeThreshold;
    public int neutralThreshold;
    public relationshipStatus relStat;
    public int relationshipValue;

    // audio related
    public string audioFolder;
    public AudioSource conversationAudio;
    public AudioSource playerAudio;

    // database related
    public IDbCommand command = null;
    public IDbConnection database = null;
    public IDataReader reader = null;

    // conversation related
    public Text displayBox = null;
    public bool inConversation = false;
    public int promptID;
    public int[] responseIDs = new int[4];
    

    public NPC(int id, string name)
    {
        this.id = id;
        this.characterName = name;
    }

    private IEnumerator ChooseResponse(
        string response, 
        int nextPromptID, 
        bool end, 
        string audioFile, 
        int relationshipEffect
    )
    {
        displayBox.text = response;

        // Let NPC finish audio before continuing
        if (conversationAudio.isPlaying)
        {
            yield return new WaitForSeconds(conversationAudio.clip.length - conversationAudio.time);
        }

        // Play the voice line for the response
        if (audioFile != "")
        {
            string responseAudioSource = "Audio/Player/" + audioFile;
            AudioClip responseAudio = Resources.Load<AudioClip>(responseAudioSource);
            playerAudio.clip = responseAudio;
            playerAudio.Play();
            yield return new WaitForSeconds(responseAudio.length + 1);
        }

        UpdateRelationshipValue(relationshipEffect);
        UpdateNextPrompt(nextPromptID);

        if (!end)
        {
            WritePrompt();
            WriteResponses();
        }
        else
        {
            inConversation = false;
        }
    }

    public void InitializeDatabase()
    {
        string connection = "URI=file:" + Application.dataPath + "/Database.db";
        database = (IDbConnection)new SqliteConnection(connection);
        database.Open();
        command = database.CreateCommand();

        // Finds the ID of the initial prompt
        string query = "SELECT PromptID, AudioFolder, RelationshipValue, DislikeThreshold, " +
            "NeutralThreshold, LikeThreshold FROM 'Characters' WHERE ID == " + id;
        command.CommandText = query;
        reader = command.ExecuteReader();

        reader.Read();
        promptID = reader.GetInt32(0);
        audioFolder = reader.GetString(1);
        relationshipValue = reader.GetInt32(2);
        dislikeThreshold = reader.GetInt32(3);
        neutralThreshold = reader.GetInt32(4);
        likeThreshold = reader.GetInt32(5);
        reader.Close();

        UpdateRelationshipStatus();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if (inConversation && Input.anyKeyDown)
        {
            int choice;

            if (!int.TryParse(Input.inputString, out choice))
            {
                return;
            }

            if (choice < 1 || choice > 4)
            {
                return;
            }

            if (responseIDs[choice - 1] == -1)
            {
                return;
            }

            string query = "SELECT DisplayText, NextPromptID, End, AudioFile, RelationshipEffect" +
                " FROM Responses WHERE ID ==" + responseIDs[choice - 1];
            command.CommandText = query;
            reader = command.ExecuteReader();

            reader.Read();
            string response = "You chose: " + reader.GetString(0);
            int nextPromptID = reader.GetInt32(1);
            bool end = reader.GetBoolean(2);
            string responseAudio = reader.IsDBNull(3) ? "" : reader.GetString(3);
            int relEffect = reader.GetInt32(4);
            reader.Close();

            StartCoroutine(ChooseResponse(response, nextPromptID, end, responseAudio, relEffect));
        }
    }

    public void StartConversation()
    {
        InitializeDatabase();
        WritePrompt();
        WriteResponses();
    }

    public void UpdateNextPrompt(int promptID)
    {
        bool shouldClose = false;
        if (command == null)
        {
            string connection = "URI=file:" + Application.dataPath + "/Database.db";
            database = (IDbConnection)new SqliteConnection(connection);
            database.Open();
            command = database.CreateCommand();
            shouldClose = true;
        }

        this.promptID = promptID;
        string update = "UPDATE Characters SET PromptID = " + promptID + " WHERE ID ==" + id;
        command.CommandText = update;
        command.ExecuteNonQuery();

        if (shouldClose)
        {
            command.Dispose();
            command = null;

            database.Close();
            database = null;
        }
    }

    private void UpdateRelationshipStatus()
    {
        if (relationshipValue <= dislikeThreshold)
        {
            relStat = relationshipStatus.dislike;
        }
        else if (relationshipValue <= neutralThreshold)
        {
            relStat = relationshipStatus.neutral;
        }
        else
        {
            relStat = relationshipStatus.like;
        }
    }

    private void UpdateRelationshipValue(int relationshipEffect)
    {
        relationshipValue += relationshipEffect;

        // relationship value can't go below/above pre-determined values
        relationshipValue = (relationshipValue < RelationshipLowerBound) ? 0 : relationshipValue;
        relationshipValue = (relationshipValue > RelationshipUpperBound) ? 10 : relationshipValue;

        UpdateRelationshipStatus();
        string update = "UPDATE Characters SET RelationshipValue = " + relationshipValue +
            " WHERE ID ==" + id;
        command.CommandText = update;
        command.ExecuteNonQuery();
    }

    public void WritePrompt()
    {
        if (promptID != -1)
        {
            string query = "SELECT DisplayText, Response1ID, Response2ID, Response3ID, Response4ID, " +
            "AudioFile FROM Prompts WHERE ID ==" + promptID;
            command.CommandText = query;
            reader = command.ExecuteReader();

            reader.Read();
            displayBox.text = reader.GetString(0);
            responseIDs[0] = reader.GetInt32(1);
            responseIDs[1] = reader.GetInt32(2);
            responseIDs[2] = reader.GetInt32(3);
            responseIDs[3] = reader.GetInt32(4);
            string audioFile = reader.IsDBNull(3) ? "" : reader.GetString(5);
            reader.Close();

            // Play the voice line for the prompt
            string promptAudioSource = "Audio/" + audioFolder + "/" + audioFile;
            AudioClip promptAudio = Resources.Load<AudioClip>(promptAudioSource);
            conversationAudio.clip = promptAudio;
            conversationAudio.Play();
        }
    }

    public void WriteResponses()
    {
        string[] responseDisplays = new string[4];
        int currentDisplay = 0;
        foreach (int id in responseIDs)
        {
            if (id > -1)
            {
                string query = "SELECT DisplayText FROM Responses WHERE ID ==" + id;
                command.CommandText = query;
                reader = command.ExecuteReader();

                reader.Read();
                responseDisplays[currentDisplay] = reader.GetString(0);
                reader.Close();
            }
            else
            {
                responseDisplays[currentDisplay] = "";
            }

            currentDisplay++;
        }

        for (int i = 1; i <= responseDisplays.Length; i++)
        {

            if (responseDisplays[i - 1] != "")
            {
                displayBox.text += "\n " + i + ". " + responseDisplays[i - 1];
            }
        }
    }
}
