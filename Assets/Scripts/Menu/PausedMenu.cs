using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject pausedmenu;
    public static bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pausedmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }

}
