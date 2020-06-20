using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MyPauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public PPlayer p;
    public GameObject RUSureScreen;
    public TMPro.TextMeshProUGUI qName, qDescription;
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        DisplayQuestInfo();
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void GoToMenu()
    {
        GameObject gsm;
        gsm = GameObject.FindGameObjectWithTag("GameSaveManager");
        gsm.GetComponent<SaveGame>().GoToMainmenu();
    }
    public void TheyWantToQuit()
    {
        pauseMenuUI.SetActive(false);
        RUSureScreen.SetActive(true);
    }
    public void DontQuitGame()
    {
        RUSureScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
        
    }
    public void QuitGame()
    {
        Debug.Log("Quit, i guess");
        Application.Quit();
    }
    void DisplayQuestInfo()
    {
        if (p.quest.isActive == true)
        {
            qName.text = p.quest.title;
            qDescription.text = p.quest.description;
        }
        else
        {
            qName.text = "No active quests";
        }
    }
}
