using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Accusation : MonoBehaviour
{
    private enum evidenceType { motive, means, opportunity, alibi, done, Mavis, MavisDone };

    private int characterID;
    private ConversationUI conversationUI;
    private evidenceType currentEvidence;
    public DatabaseHandler dbHandler;
    private Dictionary<evidenceType, List<Evidence>> evidenceMappings = new Dictionary<evidenceType, List<Evidence>>();
    private List<int> selectedEvidenceIDs = new List<int>();

    // audio related
    private string audioFolder;
    private AudioSource characterAudio;
    private string evidenceNotFoundAudio;
    private AudioSource playerAudio;

    private int endScene = 3;

    private void ClearLoadedEvidence()
    {
        evidenceMappings.Clear();
    }

    private void DescribeEvidence(int choice)
    {
        string audioPath = "Audio/Player/" + evidenceNotFoundAudio;
        Evidence chosenEvidence = evidenceMappings[currentEvidence][choice - 1];

        if (chosenEvidence.found)
        {
            selectedEvidenceIDs.Add(evidenceMappings[currentEvidence][choice - 1].id);
            string evidenceAudio = chosenEvidence.audioFile;
            audioPath = "Audio/Player/" + evidenceAudio;
        }

        // Play player audio
        if (!audioPath.EndsWith("/"))
        {
            conversationUI.PlayAudio(playerAudio, audioPath);
        }
    }

    private IEnumerator FinalConversation(int choice)
    {
        string playerLine = "Audio/Player/";
        if (choice == 1)
        {
            playerLine += "D_67";
        }
        else
        {
            playerLine += "D_68";
        }

        conversationUI.PlayAudio(playerAudio, playerLine);
        yield return new WaitForSeconds(playerAudio.clip.length);

        UnityEngine.SceneManagement.SceneManager.LoadScene(endScene);
    }

    private void GiveOptions()
    {
        string[] options = new string[evidenceMappings[currentEvidence].Count];
        options[0] = "Unknown";
        int tempOptNum = 0;

        foreach (Evidence evidence in evidenceMappings[currentEvidence])
        {
            if (evidence.found)
            {
                options[tempOptNum] = evidence.description;
                tempOptNum++;
            }
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
        string query = "SELECT Type, Found, Audio, Description, ID FROM 'Evidence' WHERE CharacterID == " + characterID;
        IDataReader reader = dbHandler.ExecuteQuery(query);

        while (reader.Read())
        {
            string type = reader.GetString(0).ToLower();
            bool found = reader.GetBoolean(1);
            string audio = reader.IsDBNull(2) ? "" : reader.GetString(2);
            string description = reader.GetString(3);
            int id = reader.GetInt32(4);

            evidenceType evType = (evidenceType)Enum.Parse(typeof(evidenceType), type);
            Evidence newEvidence = new Evidence(audio, description, found, id);
            try
            {
                List<Evidence> newEvidenceSection = new List<Evidence>();
                newEvidenceSection.Add(newEvidence);
                evidenceMappings.Add(evType, newEvidenceSection);
            }
            catch (ArgumentException)
            {
                // Add it to the list that's already there
                evidenceMappings[evType].Add(newEvidence);
            }
        }
        reader.Close();
    }

    // Returns true if still in accusation, false otherwise
    public bool SelectChoice(int choice)
    {
        conversationUI.ClearOptions();

        if (currentEvidence == evidenceType.done)
        {
            conversationUI.ClearDisplay();

            if (choice == 1)
            {
                // END GAME STATE - AN ACCUSATION HAS BEEN MADE
                EndSceneInfo.characterID = characterID;
                foreach (int evidence in selectedEvidenceIDs)
                {
                    EndSceneInfo.selectedEvidence.Add(evidence);
                }

                // If character isn't Mavis - Mavis has a final conversation.
                if (characterID != 4)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(endScene);
                }
            }
            else
            {
                // Chose not to accuse
                return false;
            }
        }
        else if (currentEvidence == evidenceType.Mavis)
        {
            StartCoroutine(FinalConversation(choice));
            return true;
        }
        else if (currentEvidence == evidenceType.MavisDone)
        {
            // Keep blocking until they finish the rest of the conversation
            return true;
        }
        else
        {
            DescribeEvidence(choice); 
        }

        currentEvidence++;
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
        conversationUI.endScene = endScene;
    }

    // Initialize character-related values and begin accusation
    public void StartAccusation()
    {
        conversationUI.ClearOptions();
        currentEvidence = evidenceType.motive;

        ClearLoadedEvidence();
        InitializeEvidence();
        StartCoroutine(TalkAbout(currentEvidence));
    }

    private IEnumerator TalkAbout(evidenceType evidenceType)
    {
        if (playerAudio.isPlaying)
        {
            yield return new WaitForSeconds(playerAudio.clip.length - playerAudio.time);
        }

        if (evidenceType == evidenceType.Mavis)
        {
            conversationUI.accused = true;
            string mavisLine = "Audio/Mavis/";

            if (EndScene.CorrectEnding(characterID, selectedEvidenceIDs))
            {
                mavisLine += "MF_26";
            }
            else
            {
                mavisLine += "MF_25";
            }

            //Accusing Mavis, one last conversation.
            conversationUI.PlayAudio(characterAudio, mavisLine);
            yield return new WaitForSeconds(characterAudio.clip.length);

            string[] options =
            {
                    "Indifferent",
                    "Sympathetic"
            };
            conversationUI.DisplayResponseOptions(options);
        }
        else
        {
            string query = "SELECT Response, ResAudio, NotFoundAudio FROM 'Accusations' WHERE CharacterID == "
                    + characterID + " " + "AND Type == '" + evidenceType.ToString() + "'";
            IDataReader reader = dbHandler.ExecuteQuery(query);

            reader.Read();
            conversationUI.UpdateDisplay(reader.GetString(0));
            string resAudio = reader.IsDBNull(1) ? "" : reader.GetString(1);
            evidenceNotFoundAudio = reader.IsDBNull(2) ? "" : reader.GetString(2);
            reader.Close();

            if (evidenceType == evidenceType.done)
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
    }
}
