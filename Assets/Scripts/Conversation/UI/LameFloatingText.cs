using UnityEngine;
using UnityEngine.UI;

public class LameFloatingText : MonoBehaviour
{
    private NPC character;
    private int optionNum;
    private Text text;

    // Displays the given conversation option where you want it
    public void DisplayOption(
        int option,
        string display,
        NPC character,
        Vector3 offset)
    {
        text = GetComponent<Text>();

        // Set the necessary values
        optionNum = option;
        this.character = character;

        // Then Move it (still no text displayed)
        transform.localScale = new Vector3(.05f, .05f, .05f);
        transform.localPosition = new Vector3(0, 1f, 0);
        ShiftPosition(offset);

        // Finally, display the given text
        text.text = display;
    }

    // Gives the selected option back to the NPC script
    public void OnClick()
    {
        // player decides to accuse NPC
        if (text.text == "Choose Evidence")
        {
            character.StartAccusation();
        }
        // player is actively accusing NPC
        else if (character.isAccusing)
        {
            character.ContinueAccusation(optionNum);
        }
        // player is in normal conversation with NPC
        else
        {
            character.ChooseResponse(optionNum);
        }
    }

    private void ShiftPosition(Vector3 offset)
    {
        transform.localPosition += offset;
    }
}
