using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuStart: MonoBehaviour
{
    public GameObject loadingPanel;

    public void StartGame()
    {
        loadingPanel.SetActive(true);
    }

}
