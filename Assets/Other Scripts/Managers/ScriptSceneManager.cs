using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Time.timeScale = 1;

        BGMusicManager.Instance.RemoveAllSound();
        BGMusicManager.Instance.PlaySound(BGMusicManager.Instance.mainmenuSource, BGMusicManager.Instance.mainmenuClip);

        SceneManager.LoadScene(0);
    }

    public void GoToGameplay()
    {
        Time.timeScale = 1;

        BGMusicManager.Instance.RemoveAllSound();
        BGMusicManager.Instance.PlaySound(BGMusicManager.Instance.gameplaySource, BGMusicManager.Instance.gameplayClip);

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
