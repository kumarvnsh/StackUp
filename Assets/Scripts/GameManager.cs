using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public  Text HSText;
    public GameObject GameOverPanel;
    public GameObject PauseMenuPanel;

    public GamePlayController GamePlayController;
    
    private void Start()
    {
        HSText.text = "HIGHSCORE:" + PlayerPrefs.GetInt("highscore");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
        GamePlayController.points = 0;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    
    
    
}
