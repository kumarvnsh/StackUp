using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{

    private float minX = -1.7f, maxX = 1.7f;

    private bool canMove;
    private float move_Speed = 2f;

    private Rigidbody2D rb;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        if (Random.Range(0 , 2) > 0)
        {
            move_Speed *= -1f;
        }

        GamePlayController.instance.currentBox = this;

    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += move_Speed * Time.deltaTime;

            if (temp.x > maxX)
            {
                move_Speed *= -1f;
            }

            if (temp.x < minX)
            {
                move_Speed *= -1f;
            }

            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
    }

    public void Landed()
    {
        if (gameOver)
        {
            return;
        }

        ignoreCollision = true;
        ignoreTrigger = true;

        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GamePlayController.instance.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision == true)
        {
            return;
        }

        if (target.gameObject.CompareTag("Platform"))
        {
            Invoke(nameof(Landed), 1f);
            ignoreCollision = true;
            GamePlayController.points += 1;
        }
        
        if (target.gameObject.CompareTag("Box"))
        {
            Invoke(nameof(Landed), 1f);
            ignoreCollision = true;
            GamePlayController.points += 1;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
        {
            return;
        }

        if (target.CompareTag("GameOver"))
        {
            CancelInvoke(nameof(Landed));
            Invoke(nameof(RestartGame), 0.5f);
            //GameManager.GameOverPanel.SetActive(true);
            //Time.timeScale = 1f;
            gameOver = true;
            ignoreTrigger = true;
            PlayFabManager.SendLeaderboard(GamePlayController.points);

        }
    }
}
