using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject pausedmenu;
    public GameObject endGameMenu;
    public static bool isPause;
    public static bool isGameEnded;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        isGameEnded = false;
        pausedmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isGameEnded)
        {
            if(isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        isPause = false;
        pausedmenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPause = true;
        pausedmenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BacktoMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        ResumeGame();
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }

    public void GameEnded()
    {
        isGameEnded = true;
        endGameMenu.SetActive(true);
    }
}
