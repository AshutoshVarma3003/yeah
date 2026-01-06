using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Asyncloader : MonoBehaviour
{
    public Slider progressBar;

    public void Start()
    {
        StartCoroutine(LoadOpenWorld());
    }

    IEnumerator LoadOpenWorld()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("OpenWorld");
        op.allowSceneActivation = false;
        Debug.Log("Opening game!");
        while (!op.isDone)
        {
            progressBar.value = Mathf.Clamp01(op.progress / 0.9f);

            if (op.progress >= 0.9f)
                op.allowSceneActivation = true;

            yield return null;
        }
    }
}
