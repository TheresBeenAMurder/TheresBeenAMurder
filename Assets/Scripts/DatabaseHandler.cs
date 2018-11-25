﻿using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

// Used for all necessary database interactions
public class DatabaseHandler: MonoBehaviour
{
    private IDbCommand command = null;
    private IDbConnection database = null;
    private IDataReader reader = null;

    public void ExecuteNonQuery(string update)
    {
        command.CommandText = update;
        command.ExecuteNonQuery();
    }

    public IDataReader ExecuteQuery(string query)
    {
        command.CommandText = query;
        reader = command.ExecuteReader();
        return reader;
    }

    public void OnDestroy()
    {
        // Close the database before ending the scene completely
        ShutDownDatabase();
    }

    // Sets up and shuts down the database for a non-query
    // update if they are currently shut down.
    public void OpenUpdateClose(string update)
    {
        bool openClose = (database == null);
        if (openClose)
        {
            SetUpDatabase();
        }

        ExecuteNonQuery(update);

        if (openClose)
        {
            ShutDownDatabase();
        }
    }

    // Resets the following values:
    // In Characters:
    //      PromptID -> DefaultPromptID
    //      RelationshipValue -> DefaultRelValue
    public void ResetDatabaseToDefault(int id)
    {
        SetUpDatabase();

        string query = "SELECT DefaultPromptID, DefaultRelValue FROM 'Characters' WHERE ID == " + id;
        IDataReader reader = ExecuteQuery(query);

        reader.Read();
        int firstPromptID = reader.GetInt32(0);
        int relationshipVal = reader.GetInt32(1);
        reader.Close();

        string update = "UPDATE Characters SET PromptID = " + firstPromptID + " WHERE ID ==" + id;
        ExecuteNonQuery(update);

        update = "UPDATE Characters SET RelationshipValue = " + relationshipVal + " WHERE ID ==" + id;
        ExecuteNonQuery(update);

        ShutDownDatabase();
    }

    public void SetUpDatabase()
    {
        string connection = "URI=file:" + Application.streamingAssetsPath + "/Database.db";
        database = (IDbConnection)new SqliteConnection(connection);
        database.Open();
        command = database.CreateCommand();
    }

    public void ShutDownDatabase()
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
    }
}
