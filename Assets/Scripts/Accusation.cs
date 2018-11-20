using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Accusation : MonoBehaviour
{
    private enum evidenceType { motive, means, opportunity, alibi, initial, done };

    private ConversationUI conversationUI;
    private int characterID;
    private evidenceType currentEvidence = evidenceType.initial;
    private DatabaseHandler dbHandler;
    private Dictionary<evidenceType, Evidence> evidenceMappings = new Dictionary<evidenceType, Evidence>();

    // audio related
    private string audioFolder;
    private AudioSource characterAudio;
    private string evidenceNotFoundAudio;
    private AudioSource playerAudio;

    private IEnumerator DescribeEvidence()
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
            yield return StartCoroutine(conversationUI.PlayAudio(playerAudio, audioPath));
        }
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
            evidenceMappings.Add(evType, newEvidence);
        }
        reader.Close();
    }

    // Returns true if still in accusation, false otherwise
    public bool SelectChoice(int choice)
    {
        if (choice < 1 || choice > 2)
        {
            return true;
        }

        // Options for first two are yes to accuse, no to leave conversation
        if (currentEvidence == evidenceType.initial)
        {
            // chose to not accuse, no longer in accusation
            if (choice == 2)
            {
                return false;
            }
        }
        else if (currentEvidence == evidenceType.done)
        {
            if (choice == 1)
            {
                // END GAME STATE - AN ACCUSATION HAS BEEN MADE
                Debug.Log("THE END GAME STATE WAS REACHED");
            }

            return false;
        }
        else
        {
            StartCoroutine(DescribeEvidence());
        }

        UpdateNextEvidence();
        StartCoroutine(TalkAbout(currentEvidence));
        return true;
    }

    // Initialize character-related values and begin accusation
    public void StartAccusation(ConversationUI convo,
        int charID,
        DatabaseHandler db,
        string charAudioFolder,
        AudioSource characterAudio,
        AudioSource playerAudio)
    {
        conversationUI = convo;
        characterID = charID;
        dbHandler = db;
        audioFolder = charAudioFolder;
        this.characterAudio = characterAudio;
        this.playerAudio = playerAudio;
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
                + characterID + ", Type == " + evidenceType.ToString();
        IDataReader reader = dbHandler.ExecuteQuery(query);

        reader.Read();
        conversationUI.UpdateDisplay(reader.GetString(0));
        string resAudio = reader.IsDBNull(1) ? "" : reader.GetString(1);
        evidenceNotFoundAudio = reader.IsDBNull(2) ? "" : reader.GetString(2);
        reader.Close();

        if (resAudio != "")
        {
            string audioPath = "Audio/" + audioFolder + "/" + resAudio;
            yield return StartCoroutine(conversationUI.PlayAudio(characterAudio, audioPath));
        }
    }

    // Makes sure we do each kind of evidence when accusing someone.
    // Yes, I know it's hacky and it sucks but I'm trying here.
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
                currentEvidence = evidenceType.done;
                break;
        }
    }
}
