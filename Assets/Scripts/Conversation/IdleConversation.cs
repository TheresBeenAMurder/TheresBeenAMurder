using System.Collections;
using UnityEngine;

public class IdleConversation : MonoBehaviour
{
    public AutoConversation[] conversations;
    public PlayerConversation playerConversation;

    int currentConvo = 0;

    public IEnumerator PlayIdleConversations()
    {
        while (currentConvo < 3)
        {
            int noTalking = 0;
            while (noTalking < 300)
            {
                yield return new WaitForSeconds(10);

                if (playerConversation.inConversation)
                {
                    noTalking = 0;
                }
                else
                {
                    noTalking += 10;
                }
            }

            yield return conversations[currentConvo].PlayDialogue();
            currentConvo++;
        }
    }
}
