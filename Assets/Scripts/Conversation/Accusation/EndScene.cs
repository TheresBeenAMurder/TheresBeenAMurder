using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip endSceneCorrect;
    public AudioClip endScenePartiallyCorrect;
    public AudioClip endSceneWrong;

    private static int correctCharacterID = 4;
    private static List<int> correctEvidenceIDs = new List<int> { 9, 11, 12, 13 };

    public static bool CorrectEnding(int accusedCharacter, List<int> utilizedEvidence)
    {
        if (EndScene.correctCharacterID == accusedCharacter)
        {
            foreach (int evidence in EndScene.correctEvidenceIDs)
            {
                if (!utilizedEvidence.Contains(evidence))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public void Start()
    {
        int characterID = EndSceneInfo.characterID;
        List<int> evidence = EndSceneInfo.selectedEvidence;

        TriggerEndDialogue(characterID, evidence);
    }

    private void TriggerEndDialogue(int accusedCharacter, List<int> utilizedEvidence)
    {
        AudioClip endScene = endSceneWrong;
        
        if (CorrectEnding(accusedCharacter, utilizedEvidence))
        {
            endScene = endSceneCorrect;
        }
        else if (correctCharacterID == 4)
        {
            endScene = endScenePartiallyCorrect;
        }

        audioSource.clip = endScene;
        audioSource.Play();
    }
}
