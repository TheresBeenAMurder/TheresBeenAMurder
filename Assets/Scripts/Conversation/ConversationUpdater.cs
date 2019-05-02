using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationUpdater : MonoBehaviour
{
    public AudioSource chiefAudio;
    public NPC madeline;
    public AudioSource madelineAudio;
    public NPC mavis;
    public AudioSource mavisAudio;
    public AudioSource playerAudio;
    public NPC victor;
    public AudioSource victorAudio;

    private Dictionary<Character, CharacterInfo> characterInfos;

    public void CloseConversation(int convoNum, bool removeMidIfPossible = false)
    {
        Conversation convo = ConversationMapper.GetConversation(convoNum);
        characterInfos[convo.character].npc.RemoveAvailableConversation(convo.startingPrompt);

        // Removes a conversation even if the player is halfway through.
        if (removeMidIfPossible)
        {
            int nextConvoPrompt = ConversationMapper.GetConversation(convoNum + 1).startingPrompt;
            for (int i = convo.startingPrompt; i < nextConvoPrompt; i++)
            {
                if (characterInfos[convo.character].npc.HasStartingPrompt(i))
                {
                    characterInfos[convo.character].npc.RemoveAvailableConversation(i);
                }
            }
        }
    }

    public void OpenConversation(int convoNum)
    {
        Conversation convo = ConversationMapper.GetConversation(convoNum);
        characterInfos[convo.character].npc.AddAvailableConversation(convo.startingPrompt);
    }

    public void Start()
    {
        characterInfos = new Dictionary<Character, CharacterInfo>(4);

        characterInfos[Character.Madeline] = new CharacterInfo
        {
            npc = madeline,
            audio = madelineAudio
        };

        characterInfos[Character.Mavis] = new CharacterInfo
        {
            npc = mavis,
            audio = mavisAudio
        };

        characterInfos[Character.Detective] = new CharacterInfo
        {
            npc = null,
            audio = playerAudio
        };

        characterInfos[Character.Victor] = new CharacterInfo
        {
            npc = victor,
            audio = victorAudio
        };

        characterInfos[Character.Chief] = new CharacterInfo
        {
            npc = null,
            audio = chiefAudio
        };
    }

    public void TriggerVoiceLine(Character character,  AudioClip clip)
    {
        characterInfos[character].audio.clip = clip;
        characterInfos[character].audio.Play();
    }

    // Waits for the entirety of a character's current voiceline.
    public IEnumerator WaitForVoiceLine(Character character)
    {
        yield return new WaitForSeconds(characterInfos[character].audio.clip.length);
    }

    public enum Character
    {
        Detective,
        Madeline,
        Mavis,
        Victor,
        Chief
    }

    public struct CharacterInfo
    {
        public NPC npc;
        public AudioSource audio;
    }

    public struct Conversation
    {
        public Character character;
        public int startingPrompt;
    }

    // Maps the known conversations (1=14) to their respective info as shown in json files.
    private class ConversationMapper
    {
        private static Dictionary<int, Conversation> conversations = new Dictionary<int, Conversation>
        {
            {
                1,
                new Conversation
                {
                    character = Character.Madeline,
                    startingPrompt = 60
                }
            },
            {
                2,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 64
                }
            },
            {
                3,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 66
                }
            },
            {
                4,
                new Conversation
                {
                    character = Character.Madeline,
                    startingPrompt = 68
                }
            },
            {
                5,
                new Conversation
                {
                    character = Character.Mavis,
                    startingPrompt = 70
                }
            },
            {
                6,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 74
                }
            },
            {
                7,
                new Conversation
                {
                    character = Character.Madeline,
                    startingPrompt = 84
                }
            },
            {
                8,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 91
                }
            },
            {
                9,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 98
                }
            },
            {
                10,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 103
                }
            },
            {
                11,
                new Conversation
                {
                    character = Character.Mavis,
                    startingPrompt = 104
                }
            },
            {
                12,
                new Conversation
                {
                    character = Character.Madeline,
                    startingPrompt = 116
                }
            },
            {
                13,
                new Conversation
                {
                    character = Character.Victor,
                    startingPrompt = 120
                }
            },
            {
                14,
                new Conversation
                {
                    character = Character.Mavis,
                    startingPrompt = 124
                }
            },
            { // NOT A REAL CONVERSATION, NEEDED FOR OTHER LOGIC
                15,
                new Conversation
                {
                    character = Character.Detective,
                    startingPrompt = 127
                }
            }
        };

        public static Conversation GetConversation(int conversationNumber)
        {
            if (!conversations.ContainsKey(conversationNumber))
            {
                throw new System.Exception("Unknown conversation requested.");
            }

            return conversations[conversationNumber];
        }
    }
}
