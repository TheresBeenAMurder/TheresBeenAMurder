using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

// This is a base class meant to be used for all NPC characters
public class NPC : MonoBehaviour {

    private static int RelationshipLowerBound = 0;
    private static int RelationshipUpperBound = 10;

    public enum relationshipStatus {hate, dislike, neutral, like};

    // NPC traits
    public string characterName;
    public int id;

    private int dislikeThreshold;
    private int likeThreshold;
    private int neutralThreshold;
    private relationshipStatus relStat;
    private int relationshipValue;

    // audio related
    public string audioFolder;
    public AudioSource conversationAudio;
    public AudioSource playerAudio;
    public SoundtrackLayer soundtrackLayer;

    // database related
    public DatabaseHandler dbHandler;

    // conversation related
    private Accusation accusation;
    public bool canAccuse = false;
    private int currentAccuseChoice = 0;
    public bool isAccusing = false;
    private int promptID;
    private int[] responseIDs = new int[4];

    // UI related
    private ConversationUI conversationUI;

    // Gets the relevant information from the database about the chosen
    // response and moves the conversation along
    public void ChooseResponse(int choice)
    {
        conversationUI.ClearOptions();

        string query = "SELECT NextPromptID, End, AudioFile, RelationshipEffect" +
                " FROM Responses WHERE ID ==" + responseIDs[choice - 1];
        IDataReader reader = dbHandler.ExecuteQuery(query);

        reader.Read();
        int nextPromptID = reader.GetInt32(0);
        bool end = reader.GetBoolean(1);
        string responseAudio = reader.IsDBNull(2) ? "" : reader.GetString(2);
        int relationshipEffect = reader.GetInt32(3);
        reader.Close();

        // Play the voice line for the response
        if (responseAudio != "")
        {
            string responseAudioSource = "Audio/Player/" + responseAudio;
            conversationUI.PlayAudio(playerAudio, responseAudioSource);
        }

        UpdateRelationshipValue(relationshipEffect);
        UpdateNextPrompt(nextPromptID);

        if (!end)
        {
            StartCoroutine(WritePrompt());
        }
        else
        {
            conversationUI.EndConversation();
        }
    }

    public void ContinueAccusation(int choice)
    {
        isAccusing = accusation.SelectChoice(choice);
        if (!isAccusing)
        {
            conversationUI.EndConversation();
        }
    }

    public void InitializeConversation()
    {
        dbHandler.SetUpDatabase();

        // Finds the ID of the initial prompt
        string query = "SELECT PromptID, AudioFolder, RelationshipValue, DislikeThreshold, " +
            "NeutralThreshold, LikeThreshold FROM 'Characters' WHERE ID == " + id;
        IDataReader reader = dbHandler.ExecuteQuery(query);

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

    private void OnTriggerEnter(Collider other)
    {
        conversationUI.PromptForConversation(other, characterName);
    }

    private void OnTriggerExit(Collider other)
    {
        conversationUI.ExitConversation(other);
    }

    public void Start()
    {
        accusation = gameObject.GetComponent<Accusation>();
        conversationAudio = gameObject.GetComponent<AudioSource>();
        conversationUI = gameObject.GetComponent<ConversationUI>();

        // Wouldn't normally want to reset on load, only on new game
        // This reset is for our single scene testing.
        dbHandler.ResetDatabaseToDefault(id);
    }

    public void StartAccusation()
    {
        isAccusing = true;
        accusation.StartAccusation();
    }

    public void StartConversation()
    {
        conversationUI.ClearDisplay();
        InitializeConversation();
        StartCoroutine(WritePrompt(canAccuse));
    }

    public void UpdateNextPrompt(int promptID)
    {
        this.promptID = promptID;
        string update = "UPDATE Characters SET PromptID = " + promptID + " WHERE ID ==" + id;
        dbHandler.OpenUpdateClose(update);
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

        switch (relStat)
        {
            case (relationshipStatus.hate):
                {
                    soundtrackLayer.switchTrack(0);
                    break;
                }
            case (relationshipStatus.dislike):
                {
                    soundtrackLayer.switchTrack(1);
                    break;
                }
            case (relationshipStatus.neutral):
                {
                    soundtrackLayer.switchTrack(2);
                    break;
                }
            case (relationshipStatus.like):
                {
                    soundtrackLayer.switchTrack(3);
                    break;
                }
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
        dbHandler.ExecuteNonQuery(update);
    }

    public IEnumerator WritePrompt(bool addAccuseOpt = false)
    {
        if (playerAudio.isPlaying)
        {
            yield return new WaitForSeconds(playerAudio.clip.length - playerAudio.time);
        }

        if (promptID != -1)
        {
            string query = "SELECT Response1ID, Response2ID, Response3ID, Response4ID, " +
            "AudioFile FROM Prompts WHERE ID ==" + promptID;
            IDataReader reader = dbHandler.ExecuteQuery(query);

            reader.Read();
            responseIDs[0] = reader.GetInt32(0);
            responseIDs[1] = reader.GetInt32(1);
            responseIDs[2] = reader.GetInt32(2);
            responseIDs[3] = reader.GetInt32(3);
            string audioFile = reader.IsDBNull(4) ? "" : reader.GetString(4);
            reader.Close();

            // Play the voice line for the prompt
            if (audioFile != "")
            {
                string promptAudioSource = "Audio/" + audioFolder + "/" + audioFile;
                conversationUI.PlayAudio(conversationAudio, promptAudioSource);

                // Make sure the voice line plays all the way through
                yield return new WaitForSeconds(conversationAudio.clip.length);
            }

            WriteResponses(addAccuseOpt);
        }
    }

    public void WriteResponses(bool addAccuseOpt = false)
    {
        string[] responseDisplays = new string[5];
        responseDisplays[4] = "";

        int currentDisplay = 0;
        foreach (int id in responseIDs)
        {
            if (id > -1)
            {
                string query = "SELECT DisplayText FROM Responses WHERE ID ==" + id;
                IDataReader reader = dbHandler.ExecuteQuery(query);

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

        // Adds an accuse option if the player can now accuse
        if (addAccuseOpt)
        {
            for (int i = 0; i < responseDisplays.Length; i++)
            {
                if (responseDisplays[i] == null || responseDisplays[i] == "")
                {
                    responseDisplays[i] = "Accuse";
                    currentAccuseChoice = i + 1;
                    break;
                }
            }
        }
        else
        {
            currentAccuseChoice = 0;
        }

        conversationUI.DisplayResponseOptions(responseDisplays);
    }
}
