using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Accusation : MonoBehaviour
{
    private enum evidenceType { motive, means, opportunity, alibi, initial, done };

    private int characterID;
    private ConversationUI conversationUI;
    private evidenceType currentEvidence;
    public DatabaseHandler dbHandler;
    private Dictionary<evidenceType, Evidence> evidenceMappings = new Dictionary<evidenceType, Evidence>();

    // audio related
    private string audioFolder;
    private AudioSource characterAudio;
    private string evidenceNotFoundAudio;
    private AudioSource playerAudio;

    public int gameOverScene = 2;

    private void DescribeEvidence()
    {
        string audioPath = "Audio/Player/" + evidenceNotFoundAudio;

        if (evidenceMappings[currentEvidence].found)
        {
            string evidenceAudio = evidenceMappings[currentEvidence].audioFile;
            audioPath = "Audio/Player/" + evidenceAudio;
        }

        // Play player audio
        if (!audioPath.EndsWith("/"))
        {
            conversationUI.PlayAudio(playerAudio, audioPath);
        }
    }

    // Update the evidence in the database to found, the evidence is now
    // accessible through the accusation mechanic
    public void FindEvidence(string evidenceType, int characterID)
    {
        string update = "UPDATE Evidence SET Found = 1 WHERE CharacterID ==" + 
            characterID + " AND Type == '" + evidenceType + "'";
        dbHandler.OpenUpdateClose(update);
    }

    private void GiveOptions()
    {
        string[] options = new string[1];
        options[0] = "Unknown";

        if (evidenceMappings[currentEvidence].found)
        {
            options[0] = evidenceMappings[currentEvidence].description;
        }

        conversationUI.DisplayResponseOptions(options);
    }

    private void GiveYesNo()
    {
        string[] options = new string[2];
        options[0] = "Yes";
        options[1] = "No";

        conversationUI.DisplayResponseOptions(options);
    }

    // Gather the evidence against/for a specific character
    private void InitializeEvidence()
    {
        string query = "SELECT Type, Found, Audio, Description FROM 'Evidence' WHERE CharacterID == " + characterID;
        IDataReader reader = dbHandler.ExecuteQuery(query);

        while (reader.Read())
        {
            string type = reader.GetString(0).ToLower();
            bool found = reader.GetBoolean(1);
            string audio = reader.IsDBNull(2) ? "" : reader.GetString(2);
            string description = reader.GetString(3);

            evidenceType evType = (evidenceType)Enum.Parse(typeof(evidenceType), type);
            Evidence newEvidence = new Evidence(audio, description, found);
            try
            {
                evidenceMappings.Add(evType, newEvidence);
            }
            catch (ArgumentException)
            {
                // Update the value that's already there
                evidenceMappings[evType] = newEvidence;
            }
        }
        reader.Close();
    }

    // Returns true if still in accusation, false otherwise
    public bool SelectChoice(int choice)
    {
        conversationUI.ClearOptions();

        // Options for first two are yes to accuse, no to leave conversation
        if (currentEvidence == evidenceType.initial)
        {
            // chose to not accuse, no longer in accusation
            if (choice == 2)
            {
                conversationUI.ClearDisplay();
                return false;
            }
        }
        else if (currentEvidence == evidenceType.done)
        {
            if (choice == 1)
            {
                // END GAME STATE - AN ACCUSATION HAS BEEN MADE
                UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverScene);
            }

            conversationUI.ClearDisplay();
            return false;
        }
        else
        {
            DescribeEvidence();
        }

        UpdateNextEvidence();
        StartCoroutine(TalkAbout(currentEvidence));
        return true;
    }

    public void Start()
    {
        NPC character = gameObject.GetComponent<NPC>();
        characterID = character.id;
        audioFolder = character.audioFolder;
        characterAudio = character.conversationAudio;
        playerAudio = character.playerAudio;
        conversationUI = gameObject.GetComponent<ConversationUI>();
    }

    // Initialize character-related values and begin accusation
    public void StartAccusation()
    {
        conversationUI.ClearOptions();
        currentEvidence = evidenceType.initial;

        InitializeEvidence();
        StartCoroutine(TalkAbout(currentEvidence));
    }

    private IEnumerator TalkAbout(evidenceType evidenceType)
    {
        if (playerAudio.isPlaying)
        {
            yield return new WaitForSeconds(playerAudio.clip.length - playerAudio.time);
        }

        string query = "SELECT Response, ResAudio, NotFoundAudio FROM 'Accusations' WHERE CharacterID == "
                + characterID + " " + "AND Type == '" + evidenceType.ToString() + "'";
        IDataReader reader = dbHandler.ExecuteQuery(query);

        reader.Read();
        conversationUI.UpdateDisplay(reader.GetString(0));
        string resAudio = reader.IsDBNull(1) ? "" : reader.GetString(1);
        evidenceNotFoundAudio = reader.IsDBNull(2) ? "" : reader.GetString(2);
        reader.Close();

        if (evidenceType == evidenceType.initial || evidenceType == evidenceType.done)
        {
            GiveYesNo();
        }
        else
        {
            GiveOptions();
        }

        if (resAudio != "")
        {
            string audioPath = "Audio/" + audioFolder + "/" + resAudio;
            conversationUI.PlayAudio(characterAudio, audioPath);

            // Let audio play all the way through
            yield return new WaitForSeconds(characterAudio.clip.length);
        }
    }

    // Makes sure we go through each kind of evidence when accusing someone.
    private void UpdateNextEvidence()
    {
        switch (currentEvidence)
        {
            case evidenceType.initial:
                currentEvidence = evidenceType.motive;
                break;
            case evidenceType.motive:
                currentEvidence = evidenceType.means;
                break;
            case evidenceType.means:
                currentEvidence = evidenceType.opportunity;
                break;
            case evidenceType.opportunity:
                currentEvidence = evidenceType.alibi;
                break;
            case evidenceType.alibi:
                currentEvidence = evidenceType.done;
                break;
        }
    }
}
