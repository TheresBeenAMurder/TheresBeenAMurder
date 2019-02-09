using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    private static int SOLUTIONLENGTH = 3;

    public CharacterNav[] characters;
    public Door door;
    public int[] puzzleSolution = new int[SOLUTIONLENGTH];

    private int currentChoiceNum = 0;
    private Lights lights;
    private bool loggingChoice = false;
    private int[] playerChoices = new int[SOLUTIONLENGTH];

    public Text feedback;

    public AudioSource audioSource;
    public AudioClip keyClick;
    public AudioClip incorrect;
    public AudioClip correct;

    // Figures out what color light to shine based on player's choice
    private Lights.LightOptions DetermineLightColor()
    {
        int choiceIndex = Array.IndexOf(puzzleSolution, playerChoices[currentChoiceNum]);

        if (choiceIndex == currentChoiceNum)
        {
            return Lights.LightOptions.Correct;
        }
        else if (choiceIndex > -1)
        {
            return Lights.LightOptions.IncorrectPlace;
        }
        else
        {
            return Lights.LightOptions.Incorrect;
        }
    }

    public bool LoggingChoice() 
    {
        return loggingChoice;
    }

    // Checks player's choices against solution
    private bool IsSolution()
    {
        // Make sure it can only be solved once
        if (!door.IsSolved())
        {
            for (int i = 0; i < SOLUTIONLENGTH; i++)
            {
                if (playerChoices[i] != puzzleSolution[i])
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    // Log the number the user pressed
    public IEnumerator LogChoice(int choice)
    {
        audioSource.clip = keyClick;
        audioSource.Play();
        loggingChoice = true;

        playerChoices[currentChoiceNum] = choice;
        lights.SetLight(currentChoiceNum, DetermineLightColor());

        // wait so the player can see the result of their
        // choice if it is the last option
        yield return new WaitForSeconds(.5f);

        currentChoiceNum++;
        if (currentChoiceNum == SOLUTIONLENGTH)
        {
            // Check for solution, then reset
            if (IsSolution())
            {
                audioSource.clip = correct;
                audioSource.Play();
                yield return StartCoroutine(door.Open());

                // Characters walk into the room
                foreach (CharacterNav character in characters)
                {
                    character.Move();
                }
            }
            else
            {
                audioSource.clip = incorrect;
                audioSource.Play();
            }

            ResetPuzzle();
        }

        loggingChoice = false;
    }

    private void ResetPuzzle()
    {
        currentChoiceNum = 0;
        
        lights.ResetLights();
        feedback.text = "";
        // Sets all values in playerChoices array to 0
        Array.Clear(playerChoices, 0, SOLUTIONLENGTH);
    }

	public void Start ()
    {
        lights = transform.parent.gameObject.GetComponentInChildren<Lights>();
	}
}
