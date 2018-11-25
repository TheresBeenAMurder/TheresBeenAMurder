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
    private string audioFolder;
    public AudioSource conversationAudio;
    public AudioSource playerAudio;
    public SoundtrackLayer soundtrackLayer;

    // database related
    public DatabaseHandler dbHandler;

    // conversation related
    private Accusation accusation;
    public bool canAccuse = false;
    private int currentAccuseChoice = 0;
    private bool isAccusing = false;
    private int promptID;
    private int[] responseIDs = new int[4];

    // UI related
    private ConversationUI conversationUI;

    // Gets the relevant information from the database about the chosen
    // response and moves the conversation along
    private IEnumerator ChooseResponse(int choice)
    {
        conversationUI.UpdateDisplay("" + choice);

        string query = "SELECT NextPromptID, End, AudioFile, RelationshipEffect" +
                " FROM Responses WHERE ID ==" + responseIDs[choice - 1];
        IDataReader reader = dbHandler.ExecuteQuery(query);

        reader.Read();
        int nextPromptID = reader.GetInt32(0);
        bool end = reader.GetBoolean(1);
        string responseAudio = reader.IsDBNull(2) ? "" : reader.GetString(2);
        int relationshipEffect = reader.GetInt32(3);
        reader.Close();

        // Let NPC finish audio before continuing
        if (conversationAudio.isPlaying)
        {
            yield return new WaitForSeconds(conversationAudio.clip.length - conversationAudio.time);
        }

        // Play the voice line for the response
        if (responseAudio != "")
        {
            string responseAudioSource = "Audio/Player/" + responseAudio;
            conversationUI.PlayAudio(playerAudio, responseAudioSource);
            yield return new WaitForSeconds(playerAudio.clip.length);
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
        conversationUI = new ConversationUI(gameObject.GetComponentInChildren<Text>());

        // Wouldn't normally want to reset on load, only on new game
        // This reset is for our single scene testing.
        dbHandler.ResetDatabaseToDefault(id);
    }

    public void StartConversation()
    {
        InitializeConversation();
        WritePrompt();
        WriteResponses(canAccuse);
    }

    // Handler of all input involving conversations
    protected virtual void Update ()
    {
        if (conversationUI.StartConversationCheck())
        {
            StartConversation();
        }

        if (conversationUI.inConversation && Input.anyKeyDown)
        {
            int choice = conversationUI.GetSelection();

            if (choice == -1)
            {
                return;
            }

            // throw this input to the accusation logic
            if (isAccusing)
            {
                isAccusing = accusation.SelectChoice(choice);
                if (!isAccusing)
                {
                    conversationUI.EndConversation();
                }
                return;
            }

            // Move into the accuse line of questioning, doesn't affect
            // future conversations so we have to break out into different
            // code.
            if (!isAccusing && choice == currentAccuseChoice)
            {
                isAccusing = true;
                StartCoroutine(accusation.StartAccusation(conversationUI,
                    id,
                    dbHandler,
                    audioFolder,
                    conversationAudio,
                    playerAudio));
                return;
            }

            if (responseIDs[choice - 1] == -1)
            {
                return;
            }

            StartCoroutine(ChooseResponse(choice));
        }
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

    public void WritePrompt()
    {
        if (promptID != -1)
        {
            string query = "SELECT DisplayText, Response1ID, Response2ID, Response3ID, Response4ID, " +
            "AudioFile FROM Prompts WHERE ID ==" + promptID;
            IDataReader reader = dbHandler.ExecuteQuery(query);

            reader.Read();
            conversationUI.UpdateDisplay(reader.GetString(0));
            responseIDs[0] = reader.GetInt32(1);
            responseIDs[1] = reader.GetInt32(2);
            responseIDs[2] = reader.GetInt32(3);
            responseIDs[3] = reader.GetInt32(4);
            string audioFile = reader.IsDBNull(3) ? "" : reader.GetString(5);
            reader.Close();

            // Play the voice line for the prompt
            if (audioFile != "")
            {
                string promptAudioSource = "Audio/" + audioFolder + "/" + audioFile;
                conversationUI.PlayAudio(conversationAudio, promptAudioSource);
            }
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
