using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public AudioSource audioSource;
    public int correctCharacterID;
    public List<int> correctEvidenceIDs = new List<int>();
    public AudioClip endSceneCorrect;
    public AudioClip endScenePartiallyCorrect;
    public AudioClip endSceneWrong;

    public void Start()
    {
        int characterID = EndSceneInfo.characterID;
        List<int> evidence = EndSceneInfo.selectedEvidence;

        TriggerEndDialogue(characterID, evidence);
    }

    private void TriggerEndDialogue(int accusedCharacter, List<int> utilizedEvidence)
    {
        AudioClip endScene = endSceneWrong;
        if (correctCharacterID == accusedCharacter)
        {
            endScene = endSceneCorrect;
            foreach (int evidence in correctEvidenceIDs)
            {
                if (!utilizedEvidence.Contains(evidence))
                {
                    endScene = endScenePartiallyCorrect;
                    break;
                }
            }
        }

        audioSource.clip = endScene;
        audioSource.Play();
    }
}
