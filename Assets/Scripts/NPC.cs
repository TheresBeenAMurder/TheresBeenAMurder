using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

// This is a base class meant to be used for all NPC characters
public class NPC : MonoBehaviour {

    private enum relationshipStatus {Hate, Dislike, Neutral, Like};

    public string audioFolder;
    public AudioSource conversationAudio;
    public int id;
    public IDbCommand command = null;
    public IDbConnection database = null;
    public Text displayBox = null;
    public bool inConversation = false;
    public string characterName;
    public AudioSource playerAudio;
    public int promptID;
    public int[] responseIDs = new int[4];
    public IDataReader reader = null;

    public NPC(int id, string name)
    {
        this.id = id;
        this.characterName = name;
    }

    private IEnumerator ChooseResponse(string response, int nextPromptID, bool end, string audioFile)
    {
        displayBox.text = response;

        // Play the voice line for the response
        if (audioFile != "")
        {
            string responseAudioSource = "Audio/Player/" + audioFile;
            AudioClip responseAudio = Resources.Load<AudioClip>(responseAudioSource);
            playerAudio.clip = responseAudio;
            playerAudio.Play();
            yield return new WaitForSeconds(responseAudio.length + 2);
        }

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
        string query = "SELECT PromptID, AudioFolder FROM 'Characters' WHERE ID == " + id;
        command.CommandText = query;
        reader = command.ExecuteReader();

        reader.Read();
        promptID = reader.GetInt32(0);
        audioFolder = reader.GetString(1);
        reader.Close();
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

            string query = "SELECT DisplayText, NextPromptID, End, AudioFile FROM Responses WHERE ID ==" +
                responseIDs[choice - 1];
            command.CommandText = query;
            reader = command.ExecuteReader();

            reader.Read();
            string response = "You chose: " + reader.GetString(0);
            int nextPromptID = reader.GetInt32(1);
            bool end = reader.GetBoolean(2);
            string responseAudio = reader.IsDBNull(3) ? "" : reader.GetString(3);
            reader.Close();

            StartCoroutine(ChooseResponse(response, nextPromptID, end, responseAudio));
        }
    }

    public void StartConversation()
    {
        InitializeDatabase();
        WritePrompt();
        WriteResponses();
    }

    private void UpdateNextPrompt(int promptID)
    {
        this.promptID = promptID;
        string update = "UPDATE Characters SET PromptID = " + promptID + " WHERE ID ==" + id;
        command.CommandText = update;
        command.ExecuteNonQuery();
    }

    public void WritePrompt()
    {
        string query = "SELECT DisplayText, Response1ID, Response2ID, Response3ID, Response4ID FROM Prompts WHERE ID ==" + promptID;
        command.CommandText = query;
        reader = command.ExecuteReader();

        reader.Read();
        displayBox.text = reader.GetString(0);
        responseIDs[0] = reader.GetInt32(1);
        responseIDs[1] = reader.GetInt32(2);
        responseIDs[2] = reader.GetInt32(3);
        responseIDs[3] = reader.GetInt32(4);
        reader.Close();

        // Play the voice line for the prompt
        string promptAudioSource = "Audio/" + audioFolder + "/" + promptID + "-Neutral";
        AudioClip promptAudio = Resources.Load<AudioClip>(promptAudioSource);
        conversationAudio.clip = promptAudio;
        conversationAudio.Play();
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
