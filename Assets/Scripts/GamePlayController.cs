using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{

    public static GamePlayController instance;

    public BoxSpawner boxSpawner;
    public AudioClip pop;

    [HideInInspector] public BoxScript currentBox;

    public CameraFollow cameraScript;
    private int moveCount;

    public Text pointsText;
    public static int points;
    
    public GameManager GameManager;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxSpawner.SpawnBox();
        //points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
        pointsText.text  = "" + points;
        
        if (points > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", points);
            
        }
    }

    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
            FindObjectOfType<AudioManager>().PlaySound("tap");
        }
    }

    public void SpawnNewBox()
    {
        Invoke(nameof(NewBox), 1f);
    }

    void NewBox()
    {
        boxSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        moveCount++;

        if (moveCount == 4f)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 1.6f;
        }
    }

    public void RestartGame()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        // points = 0;
        GameManager.GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
