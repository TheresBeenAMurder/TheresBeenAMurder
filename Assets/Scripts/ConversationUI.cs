using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    private static OVRInput.Button ConversationButton = OVRInput.Button.Two;

    public Camera centerEyeCam;
    public bool inConversation = false;
    public GameObject prefab;

    private Text displayBox = null;
    private bool playerNear = false;

    // 5 is the maximum amount of options you can have at any time 
    // (including accusation option)
    private LameFloatingText[] optionDisplays = new LameFloatingText[5];
    private GameObject[] optionObjects = new GameObject[5];

    // Figures out where in space to place the spawned option object
    private Vector3 CalculateOffset(int numOption, int totalNumOptions)
    {
        float x = 0;
        float y = 0;
        float z = 0;

        if (totalNumOptions > 1)
        {
            float middleVal;
            if (totalNumOptions % 2 == 0)
            {
                middleVal = (totalNumOptions / 2) + .5f;
            }
            else
            {
                middleVal = (totalNumOptions / 2) + 1f;
            }

            x = 10 * (numOption - middleVal);
        }

        return new Vector3(x, y, z);
    }

    public void ClearDisplay()
    {
        UpdateDisplay("");
    }

    public void ClearOptions()
    {
        foreach (GameObject opt in optionObjects)
        {
            Destroy(opt);
        }
    }

    public void DisplayResponseOptions(string[] responses)
    {
        NPC parent = gameObject.GetComponent<NPC>();
        int totalNumOptions = 0;

        for (int i = responses.Length - 1; i >= 0; i--)
        {
            if (responses[i] != "")
            {
                totalNumOptions = i + 1;
                break;
            }
        }

        for (int i = 0; i < totalNumOptions; i++)
        {
            // Spawn a prefab and set the event camera to the center eye camera
            optionObjects[i] = Instantiate(prefab, parent.transform);
            optionObjects[i].GetComponent<Canvas>().worldCamera = centerEyeCam;

            // Display the prefab with the option
            optionDisplays[i] = optionObjects[i].GetComponentInChildren<LameFloatingText>();
            optionDisplays[i].DisplayOption(i + 1, responses[i], parent, CalculateOffset(i + 1, totalNumOptions));
        }
    }

    public void EndConversation()
    {
        inConversation = false;
    }

    public void ExitConversation(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ClearDisplay();
            ClearOptions();
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

    public void PlayAudio(AudioSource source, string fullClipPath)
    {
        AudioClip audio = Resources.Load<AudioClip>(fullClipPath);
        source.clip = audio;
        source.Play();
    }

    public void PromptForConversation(Collider other, string name)
    {
        if (!playerNear && !inConversation && other.gameObject.tag == "Player")
        {
            displayBox.text = "Press B to speak to " + name;
            playerNear = true;
        }
    }

    public void Start()
    {
        displayBox = gameObject.GetComponentInChildren<Text>();
    }

    public bool StartConversationCheck()
    {
        if (!inConversation && playerNear && OVRInput.GetDown(ConversationButton))
        {
            inConversation = true;
            return true;
        }

        return false;
    }

    // Determines whether or not to start a conversation
    public void Update()
    {
        if (!inConversation && playerNear && OVRInput.GetDown(ConversationButton))
        {
            inConversation = true;
            gameObject.GetComponent<NPC>().StartConversation();
        }
    }

    public void UpdateDisplay(string prompt)
    {
        displayBox.text = prompt;
    }
}
