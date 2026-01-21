using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public PlayerMovement playerMovement;
    [SerializeField] private QuestItem questUnoItem;

    private Dictionary<string, Quest> quests = new Dictionary<string, Quest>();

    private void Awake()
    {
        Instance = this;
        RegisterQuests();
    }

    private void Start()
    {
        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void RegisterQuests()
    {
        AddQuest(new QuestUno(questUnoItem));
        // Add more quests here
    }

    void AddQuest(Quest quest)
    {
        quests.Add(quest.questId, quest);
    }

    public void OnTalkToNPC(NPC npc)
    {
        foreach (Quest quest in quests.Values)
        {
            if (!quest.isCompleted)
            {
                bool handled = quest.OnTalkToNPC(npc);
                if (handled)
                    return; // stop here, quest dialogue played
            }
        }

        // No quest handled this NPC = generic dialogue
        PlayGenericDialogue(npc);
    }
    void PlayGenericDialogue(NPC npc)
    {
        string[] genericLines =
        {
        "npc_generic_01",
        "npc_generic_02",
        "npc_generic_03"
    };

        string chosen = genericLines[Random.Range(0, genericLines.Length)];
        npc.ShowName();
        DialogueScript.Instance.PlayDialogueById(
            chosen,
            () => {
                npc.StopFacingPlayer();
                npc.HideName();
                }
        );
    }


    public void OnQuestItemCollected(string questId)
    {
        if (!quests.ContainsKey(questId)) return;

        Quest quest = quests[questId];

        quest.OnItemCollected();
    }
}
