using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameStateHandler : MonoBehaviour
{
    public static GameStateHandler current;

    public int scoreMultiplier = 10;

    [Header("UI References")]
    public TextMeshProUGUI timeTracker;
    public TextMeshProUGUI scoreTracker;

    [Header("Game Over UI")]
    public GameObject gameOverUI;

    public int currentTime;
    public int playerScore;
    int lastTime = 0;
    int displayScore;

    bool addingScore = false;

    // References
    Player player;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player = FindObjectOfType<Player>();
        gameOverUI.SetActive(false);
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerStats.GetCurrentHealth() <= 0)
        {
            GameOver();
        }
        else
        {
            displayScore = (int)Mathf.MoveTowards(displayScore, playerScore, 1000f * Time.deltaTime);
            UpdateScoreDisplay();
        }
    }

    public void FixedUpdate()
    {
        if (player.playerStats.GetCurrentHealth() <= 0)
        {
            return;
        }
        currentTime = (int)(Time.time - GameManager.current.elapsedTime);
        timeTracker.text = "Time: " + currentTime;
        lastTime = currentTime;
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void UpdateScoreDisplay()
    {
        scoreTracker.text = "Score: " + displayScore;
    }

    public void GameOver()
    {
        PauseGame();
        player.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
        playerScore += currentTime * scoreMultiplier;
        displayScore = (int)Mathf.MoveTowards(displayScore, playerScore, 1000f * Time.deltaTime);
        UpdateScoreDisplay();
    }

    public void RestartGame()
    {
        ResumeGame();
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
