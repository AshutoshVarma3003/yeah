using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class NPC : MonoBehaviour, IInteractable
{
    public string npcBaseId;
    public int dialogueIndex = 1;
    public bool isInteractable = true;
    public TextMeshProUGUI nameText;
    public enum Gender { Male, Female }
    public Gender gender;

    public string CurrentNpcId => $"{npcBaseId}_{dialogueIndex:D2}";

    public Transform player;
    public float rotationSpeed = 5f;

    Quaternion originalRotation;
    bool hasStoredOriginal;

    bool isFacingPlayer;
    bool isResettingRotation;

    void Awake()
    {
        originalRotation = transform.rotation;
        hasStoredOriginal = true;
        nameText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    void Update()
    {
        if (isFacingPlayer)
            FacePlayer();

        if (isResettingRotation)
            ResetRotation();
    }

    public void FacePlayer()
    {
        if (player == null) return;
        ShowName();
        if (!hasStoredOriginal)
        {
            originalRotation = transform.rotation;
            hasStoredOriginal = true;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation =
            Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    public void ResetRotation()
    {
        HideName() ;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            originalRotation,
            Time.deltaTime * rotationSpeed
        );

        // Stop when close enough
        if (Quaternion.Angle(transform.rotation, originalRotation) < 0.5f)
        {
            transform.rotation = originalRotation;
            isResettingRotation = false;
        }
    }

    public void StartFacingPlayer()
    {
        isResettingRotation = false;
        isFacingPlayer = true;
    }

    public void StopFacingPlayer()
    {
        isFacingPlayer = false;
        isResettingRotation = true;
    }

    public void Interact()
    {
        if (!isInteractable) return;

        StartFacingPlayer();
        QuestManager.Instance.OnTalkToNPC(this);
    }
    public void ShowName()
    {
        string[] maleNames =
    {
        "Arjun",
        "Rohan",
        "Karthik",
        "Vikram",
        "Ayaan"
    };

        string[] femaleNames =
        {
        "Ananya",
        "Meera",
        "Kavya",
        "Isha",
        "Naina"
    };
        if (nameText == null)
            return;

        // If name already exists, just show it
        if (!string.IsNullOrWhiteSpace(nameText.text))
        {
            nameText.gameObject.SetActive(true);
            return;
        }

        // Assign random name based on gender
        string chosenName;

        if (gender == Gender.Male)
        {
            chosenName = maleNames[Random.Range(0, maleNames.Length)];
        }
        else
        {
            chosenName = femaleNames[Random.Range(0, femaleNames.Length)];
        }
        Debug.Log("Setting NPC name to: " + chosenName);
        nameText.text = chosenName;
        nameText.gameObject.SetActive(true);
    }

    public void HideName()
    {
        if (nameText == null)
        {
            return;
        }
        nameText.gameObject.SetActive(false);
    }
    public void AdvanceDialogue()
    {
        dialogueIndex++;
    }
}
