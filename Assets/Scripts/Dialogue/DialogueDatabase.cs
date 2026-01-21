using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string id;
    [TextArea(2, 4)]
    public string[] lines;
}

public class DialogueDatabase : MonoBehaviour
{
    public DialogueEntry[] dialogues;

    public string[] GetLines(string id)
    {
        foreach (var d in dialogues)
        {
            if (d.id == id)
                return d.lines;
        }
        return null;
    }
}
