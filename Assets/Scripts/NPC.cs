using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

// This is a base class meant to be used for all NPC characters
public class NPC : MonoBehaviour {

    private enum relationshipStatus {Hate, Dislike, Neutral, Like};

    public int id;
    public IDbCommand command = null;
    public IDbConnection database = null;
    public Text displayBox = null;
    public bool inConversation = false;
    public new string name;
    public int promptID;
    public int[] responseIDs = new int[4];
    public IDataReader reader = null;

    public NPC(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    private IEnumerator ChooseResponse(string response, int nextPromptID, bool end)
    {
        displayBox.text = response;
        yield return new WaitForSeconds(3);

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
        string query = "SELECT PromptID FROM 'Characters' WHERE ID == " + id;
        command.CommandText = query;
        reader = command.ExecuteReader();

        reader.Read();
        promptID = reader.GetInt32(0);
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

            string query = "SELECT DisplayText, NextPromptID, End FROM Responses WHERE ID ==" +
                responseIDs[choice - 1];
            command.CommandText = query;
            reader = command.ExecuteReader();

            reader.Read();
            string response = "You chose: " + reader.GetString(0);
            int nextPromptID = reader.GetInt32(1);
            bool end = reader.GetBoolean(2);
            reader.Close();

            StartCoroutine(ChooseResponse(response, nextPromptID, end));
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
