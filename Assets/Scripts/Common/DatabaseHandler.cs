using Mono.Data.Sqlite;
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

    // Update the evidence in the database to found, the evidence is now
    // accessible through the accusation mechanic
    public void FindEvidence(string evidenceType, int characterID, int evidenceID)
    {
        string update = "UPDATE Evidence SET Found = 1 WHERE CharacterID ==" +
            characterID + " AND Type == '" + evidenceType + "' AND ID ==" + evidenceID;
        OpenUpdateClose(update);
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
    // In Evidence:
    //      Found -> 0 for all character-related evidence
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

        update = "UPDATE Evidence SET Found = 0 WHERE CharacterID == " + id;
        ExecuteNonQuery(update);

        ShutDownDatabase();
    }

    // Returns false if the database wasn't open already, true if it was
    public bool SetUpDatabase()
    {
        if (database == null)
        {
            string connection = "URI=file:" + Application.streamingAssetsPath + "/Database.db";
            database = (IDbConnection)new SqliteConnection(connection);
            database.Open();
            command = database.CreateCommand();
            return false;
        }
        return true;
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

    public void Start()
    {
        // Reset database for "new" game
        ResetDatabaseToDefault(2);
        ResetDatabaseToDefault(3);
        ResetDatabaseToDefault(4);
    }
}
