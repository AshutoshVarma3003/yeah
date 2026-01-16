using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float typingSpeed = 0.04f;
    public float clearDelay = 2.0f;

    IEnumerator TypeText(string text)
    {
        textMesh.text = text;
        textMesh.maxVisibleCharacters = 0;

        for (int i = 0; i <= text.Length; i++)
        {
            textMesh.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait after full line is shown
        yield return new WaitForSeconds(clearDelay);

        // Clear text
        textMesh.text = "";
    }
    public void PlayDialogue(string line)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(line));
    }

}
