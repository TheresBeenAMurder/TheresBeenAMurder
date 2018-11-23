using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    private static OVRInput.Button ConversationButton = OVRInput.Button.Two;

    public bool inConversation = false;

    private Text displayBox = null;
    private GameObject canvas = null;
    private bool playerNear = false;

    public ConversationUI(Text display)
    {
        displayBox = display;
    }


    public void ClearDisplay()
    {
        UpdateDisplay("");
    }

    public void DisplayResponseOptions(string[] responses)
    {
        for (int i = 1; i <= responses.Length; i++)
        {

            if (responses[i - 1] != "")
            {
                displayBox.text += "\n " + i + ". " + responses[i - 1];
            }
        }
    }

    public void EndConversation()
    {
        inConversation = false;
    }

    public void ExitConversation(Collider other, string name)
    {
        if (inConversation && other.gameObject.tag == "Player")
        {
            displayBox.text = "";
            inConversation = false;
            playerNear = false;
        }
    }

    public int GetSelection()
    {
        int choice;

        if (!int.TryParse(Input.inputString, out choice))
        {
            return -1;
        }

        if (choice < 1 || choice > 5)
        {
            return -1;
        }

        return choice;
    }

    public IEnumerator PlayAudio(AudioSource source, string fullClipPath)
    {
        AudioClip audio = Resources.Load<AudioClip>(fullClipPath);
        source.clip = audio;
        source.Play();
        yield return new WaitForSeconds(audio.length + 1);
    }

    public void PromptForConversation(Collider other, string name)
    {
        if (!playerNear && other.gameObject.tag == "Player")
        {
            displayBox.text = "Press B to speak to " + name;
            playerNear = true;
        }
    }

    public bool StartConversationCheck()
    {
        if (!inConversation && playerNear && OVRInput.GetDown(ConversationButton))
        {
            inConversation = true;
        }

        return inConversation;
    }

    public void UpdateDisplay(string prompt)
    {
        displayBox.text = prompt;
    }
}
