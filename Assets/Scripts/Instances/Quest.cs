public abstract class Quest
{
    public string questId;
    public bool isStarted;
    public bool isCompleted;

    public abstract void StartQuest();
    public abstract void CompleteQuest();

    public virtual void OnItemCollected() { }

    public virtual bool OnTalkToNPC(NPC npc) { 
        return false;
    }
}
