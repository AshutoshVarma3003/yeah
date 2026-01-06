using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class AsyncLoader : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        StartCoroutine(LoadOpenWorld());
    }

    IEnumerator LoadOpenWorld()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("OpenWorld");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Progress is 0 to 0.9 before activation
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            // Activate when fully loaded
            if (operation.progress >= 0.9f)
            {
                // Optional delay so players can see 100%
                yield return new WaitForSeconds(0.3f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
