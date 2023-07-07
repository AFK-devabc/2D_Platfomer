using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckSaveFile :MonoBehaviour
{
   [SerializeField] private string FileName;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject continueGameButton;
    private void Start()
    {
        string fullPath = Path.Combine(DataPersistenceManager.instance.filePath, FileName);
        if (File.Exists(fullPath))
        {
            continueGameButton.SetActive(true);
        }
        else
        {
            newGameButton.SetActive(true);
        }
    }

    public void SetGameNormalMode()
    {
        DataPersistenceManager.instance.SetGameMode(GameMode.Normal);
    }

    public void SetGameHardMode()
    {
        DataPersistenceManager.instance.SetGameMode(GameMode.Hard);
    }

    public void PlayClick()
    {
        DataPersistenceManager.instance.SetFileName(FileName);
        SceneManager.LoadScene(2);
    }

}
