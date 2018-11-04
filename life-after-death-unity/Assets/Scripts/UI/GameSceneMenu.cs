using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMenu : MonoBehaviour
{
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void ContinueButtonClick()
    {
        CloseMenu();
        GameSceneManager.UnPauseGame();
    }

    public void RestartAtCheckpointButtonClick()
    {
        SceneManager.LoadScene("GameScene");
        GameSceneManager.UnPauseGame();
    }

    public void QuitButtonClick()
    {
        GameSceneManager.UnPauseGame();
        SceneManager.LoadScene("MainMenu");
    }
}
