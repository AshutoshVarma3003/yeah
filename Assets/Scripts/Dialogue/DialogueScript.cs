using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public DialogueDatabase database;
    public static DialogueScript Instance;

    public float typingSpeed = 0.04f;
    public float clearDelay = 2.0f;

    private void Awake()
    {
        Instance = this;
    }
    IEnumerator TypeText(string text)
    {
        textMesh.text = text;
        textMesh.maxVisibleCharacters = 0;

        for (int i = 0; i <= text.Length; i++)
        {
            textMesh.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(clearDelay);
        textMesh.text = "";
    }

    public void PlayDialogue(string id)
    {
        StopAllCoroutines();
        string[] lines = database.GetLines(id);
        if (lines != null && lines.Length > 0)
        {
            StartCoroutine(PlayDialogueOnce(lines));
        }
    }

    IEnumerator PlayDialogueOnce(string[] lines)
    {
        Debug.Log("Starting dialogue sequence.");
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeText(line));
        }
        Debug.Log("Dialogue sequence finished.");
    }
    public void PlayDialogueById(string id, System.Action onFinished = null)
    {
        StopAllCoroutines();

        string[] lines = database.GetLines(id);
        if (lines != null && lines.Length > 0)
        {
            StartCoroutine(PlayDialogueSequence(lines, onFinished));
        }
    }

    IEnumerator PlayDialogueSequence(string[] lines, System.Action onFinished)
    {
        Debug.Log("Starting dialogue sequence.");
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeText(line));
        }
        Debug.Log("Dialogue sequence finished.");
        onFinished?.Invoke();
    }


}
