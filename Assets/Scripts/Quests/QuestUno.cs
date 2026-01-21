using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class QuestUno : Quest
{
    private QuestItem questItem;
    public bool hasItemBeenTaken = false;
    public QuestUno(QuestItem questItem)
    {
        questId = "take_parchment";
        this.questItem = questItem;
    }
    public override bool OnTalkToNPC(NPC npc)
    {
        if (npc.npcBaseId != "npc_01")
            return false;

        if (!isStarted)
        {
            QuestManager.Instance.playerMovement.isLocked = true;

            DialogueScript.Instance.PlayDialogueById(
                npc.CurrentNpcId,
                () => {
                    npc.StopFacingPlayer();
                    StartQuest();
                }
            );
            return true;
        }

        if (hasItemBeenTaken && !isCompleted)
        {
            npc.AdvanceDialogue();
            QuestManager.Instance.playerMovement.isLocked = true;

            DialogueScript.Instance.PlayDialogueById(
                npc.CurrentNpcId,
                () => {
                    npc.StopFacingPlayer();
                    CompleteQuest();
                    }
            );

            npc.isInteractable = false;
            return true;
        }
        return false;
    }


    public override void StartQuest()
    {
        QuestManager.Instance.playerMovement.isLocked = false;
        
        isStarted = true;
        questItem.gameObject.SetActive(true);
    }

    public override void OnItemCollected()
    {
        hasItemBeenTaken = true;
        //DialogueScript.Instance.PlayDialogueById("quest_01_02", CompleteQuest);
    }

    public override void CompleteQuest()
    {
        QuestManager.Instance.playerMovement.isLocked = false;
        isCompleted = true;
        Debug.Log("Fetch Quest completed!");
    }
}

