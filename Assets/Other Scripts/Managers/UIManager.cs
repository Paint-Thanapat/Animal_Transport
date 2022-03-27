using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIManager : MonoBehaviour
{
    public enum CurrentScene
    {
        OnMainMenu, OnGameplay
    }

    public CurrentScene currentScene;
    public GameObject mainmenuCanvas;
    public GameObject gameplayCanvas;
    public GameObject pauseCanvas;

    void Start()
    {
        if (currentScene == CurrentScene.OnMainMenu)
        {
            mainmenuCanvas.SetActive(true);
            gameplayCanvas.SetActive(false);
        }
        else if (currentScene == CurrentScene.OnGameplay)
        {
            mainmenuCanvas.SetActive(false);
            gameplayCanvas.SetActive(true);
        }

        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (gameplayCanvas.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPause();
            }
        }
    }

    public void SetPause()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);

        bool isPause = pauseCanvas.activeInHierarchy;

        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
