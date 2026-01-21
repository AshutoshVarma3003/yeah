using UnityEngine;

public class QuestItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string questId;
    public void Interact()
    {
        QuestManager.Instance.OnQuestItemCollected(questId);
        gameObject.SetActive(false);
    }
    public void FacePlayer()
    {
        // Quest items do not need to face the player
    }
    public void ResetRotation()
    {
        // Quest items do not need to reset position
    }
    public void StopFacingPlayer() { }
    public void StartFacingPlayer() { }

    public void ShowName()
    {
        // Optionally implement UI logic to show the item's name
    }
    public void HideName() { }
}
