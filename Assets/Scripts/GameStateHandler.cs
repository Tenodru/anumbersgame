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

    [Header("Score Screen")]
    public GameObject scoreScreen;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreModifier;

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
        scoreScreen.SetActive(false);
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
        playerScore += currentTime * scoreMultiplier;
        CalculateFinalScore();
        //displayScore = (int)Mathf.MoveTowards(displayScore, playerScore, 1000f * Time.deltaTime);
        PauseGame();
        player.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void CalculateFinalScore()
    {
        scoreText.text = playerScore.ToString();
        GameManager.current.scoreModifiers[0].score = currentTime * scoreMultiplier;
        StartCoroutine(ShowScoreModifiers(GameManager.current.scoreModifiers));
    }

    IEnumerator ShowScoreModifiers(List<ScoreModifier> modifiers, float time = 3f)
    {
        yield return new WaitForSeconds(time);
        scoreModifier.text = modifiers[0].name + ": " + modifiers[0].score;
        modifiers.RemoveAt(0);
        if (modifiers.Count >= 1)
        {
            StartCoroutine(ShowScoreModifiers(modifiers));
        }
    }

    public void ScoreScreen()
    {
        scoreScreen.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
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
