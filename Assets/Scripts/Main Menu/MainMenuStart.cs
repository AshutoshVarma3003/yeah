using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuStart: MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider progressBar;

    public void StartGame()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadOpenWorld());
    }

    IEnumerator LoadOpenWorld()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("OpenWorld");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            progressBar.value = Mathf.Clamp01(op.progress / 0.9f);

            if (op.progress >= 0.9f)
                op.allowSceneActivation = true;

            yield return null;
        }
    }
}
