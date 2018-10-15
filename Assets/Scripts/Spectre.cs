using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Spectre : MonoBehaviour {

    static int ID = 2;

    GameObject canvas = null;
    IDbCommand command = null;
    IDbConnection database = null;
    Text displayBox = null;
    bool inConversation = false;
    bool playerNear = false;
    int[] responseIDs = new int[4];
    IDataReader reader = null;

	// Use this for initialization
	void Start ()
    {
        canvas = GameObject.Find("Canvas");
        displayBox = canvas.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!inConversation && playerNear && Input.GetKey("e"))
        {
            inConversation = true;
            InitializeDatabase();
        }

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

    private IEnumerator ChooseResponse(string response, int nextPromptID, bool end)
    {
        displayBox.text = response;
        yield return new WaitForSeconds(3);
        
        UpdateNextPrompt(nextPromptID);

        if (!end)
        {
            GetPrompt(nextPromptID);
            DisplayResponses();
        }
        else
        {
            inConversation = false;
        }
    }

    private void DisplayResponses()
    {
        string[] responseDisplays = new string[4];
        int currentDisplay = 0;
        foreach (int id in responseIDs)
        {
            if (id > 0)
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

    private void GetPrompt(int promptID)
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

    private void InitializeDatabase()
    {
        string connection = "URI=file:" + Application.dataPath + "/Database.db";
        database = (IDbConnection)new SqliteConnection(connection);
        database.Open();
        command = database.CreateCommand();

        // Finds the ID of the initial prompt
        string query = "SELECT PromptID FROM 'Characters' WHERE ID == " + ID;
        command.CommandText = query;
        reader = command.ExecuteReader();

        reader.Read();
        int promptID = reader.GetInt32(0);
        reader.Close();

        GetPrompt(promptID);
        DisplayResponses();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!playerNear)
        {
            displayBox.text = "Press E to speak to Spectre";
            playerNear = true;
        }
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

    private IEnumerator PauseResponse(string response)
    {
        displayBox.text = response;
        yield return new WaitForSeconds(3);
    }

    private void UpdateNextPrompt(int promptID)
    {
        string update = "UPDATE Characters SET PromptID = " + promptID + " WHERE ID ==" + ID;
        command.CommandText = update;
        command.ExecuteNonQuery();
    }
}
