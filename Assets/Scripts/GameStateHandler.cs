using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("Post Score Screen")]
    public GameObject postScoreScreen;

    [Header("Save Score Screen")]
    public GameObject saveScoreScreen;
    public InputField playerName;

    [Header("Game Over UI")]
    public GameObject gameOverUI;

    public int currentTime;
    public int playerScore;
    public int fastScore;
    int lastTime = 0;
    int displayScore;
    float moveTowardsDur = 1000f;

    bool addingScore = false;
    bool gameEnded = false;
    bool scoreScreenOpen = false;
    bool newScore = false;

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
        saveScoreScreen.SetActive(false);
        newScore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerStats.GetCurrentHealth() <= 0)
        {
            GameOver();
            if (scoreScreenOpen)
            {
                displayScore = (int)Mathf.MoveTowards(displayScore, playerScore, moveTowardsDur * Time.unscaledDeltaTime);
                Debug.Log("Updating final score display.");
                UpdateFinalScoreDisplay();
            }
        }
        else
        {
            displayScore = (int)Mathf.MoveTowards(displayScore, playerScore, moveTowardsDur * Time.deltaTime);
            UpdateScoreDisplay();

            if (Input.GetMouseButtonDown(1))
            {
                Player.current.GetComponent<Player>().playerStats.TakeDamage(100);
            }
        }
    }

    public void FixedUpdate()
    {
        if (player.playerStats.GetCurrentHealth() <= 0)
        {
            return;
        }
        currentTime = (int)(Time.time - GameManager.current.elapsedTime - GameManager.current.elapsedTimeGame);
        timeTracker.text = "Time: " + currentTime;
        lastTime = currentTime;
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        moveTowardsDur = amount * 10;
    }

    public void UpdateScoreDisplay()
    {
        scoreTracker.text = "Score: " + displayScore;
    }

    public void UpdateFinalScoreDisplay()
    {
        scoreText.text = displayScore.ToString();
    }

    public void GameOver()
    {
        if (!gameEnded)
        {
            PauseGame();
            player.gameObject.SetActive(false);
            gameOverUI.SetActive(true);
            GameManager.current.elapsedTimeGame = currentTime;
            gameEnded = true;
            //StartCoroutine(FadeOutScoreModifier(0));
            StartCoroutine(FadeObjectOut(scoreModifier.gameObject));
        }
    }

    /// <summary>
    /// Calculates the player's final score and prepares for score display.
    /// </summary>
    public void CalculateFinalScore()
    {
        fastScore = playerScore;
        displayScore = playerScore;
        scoreText.text = displayScore.ToString();

        // Calculate full final score ahead of fancy animations.
        foreach(ScoreModifier modifier in GameManager.current.scoreModifiers)
        {
            fastScore += modifier.score;
        }

        // Check if this score is higher than any of the other scores.
        foreach (ScoreEntry entry in HighScoreManager.scores.list)
        {
            if (fastScore >= entry.score)
            {
                newScore = true;
            }
        }

        GameManager.current.scoreModifiers[0].score = currentTime * scoreMultiplier;
        StartCoroutine(ShowScoreModifiers(GameManager.current.scoreModifiers, 0f));
        StartCoroutine(FadeObjectIn(scoreModifier.gameObject));
    }

    /// <summary>
    /// Parses score modifiers list and displays each for 6 sec (by default).
    /// </summary>
    /// <param name="modifiers"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ShowScoreModifiers(List<ScoreModifier> modifiers, float time = 6f)
    {
        yield return new WaitForSecondsRealtime(time);
        // Show modifier name and score.
        scoreModifier.text = modifiers[0].name + ": " + modifiers[0].score;
        // Add modifier score to playerScore, and change moveTowardsDur.
        playerScore += modifiers[0].score;
        moveTowardsDur = modifiers[0].score * 5;

        // Remove the modifier we just showed from the modifier list.
        // Show the next modifier.
        modifiers.RemoveAt(0);
        if (modifiers.Count > 0)
        {
            Debug.Log("Showing next modifier.");
            //StartCoroutine(FadeOutScoreModifier(4));
            //StartCoroutine(FadeInScoreModifier(5));
            StartCoroutine(FadeObjectOut(scoreModifier.gameObject, waitTime: 5f));
            StartCoroutine(ShowScoreModifiers(modifiers));
            StartCoroutine(FadeObjectIn(scoreModifier.gameObject, waitTime: 6));
        } else
        {
            //StartCoroutine(FadeOutScoreModifier(4));
            StartCoroutine(FadeObjectOut(scoreModifier.gameObject, waitTime: 4f));
        }
    }

    /// <summary>
    /// Fades the object alpha out to 0 over time.
    /// </summary>
    /// <param name="dur"></param>
    /// <returns></returns>
    IEnumerator FadeObjectOut(GameObject obj, float dur = 0.5f, float waitTime = 0f)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float startVal = canvasGroup.alpha;
        float time = 0;

        while (time < dur)
        {
            canvasGroup.alpha = Mathf.Lerp(startVal, 0, time / dur);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// Fades the object alpha in to 1 over time.
    /// </summary>
    /// <param name="dur"></param>
    /// <returns></returns>
    IEnumerator FadeObjectIn(GameObject obj, float dur = 0.5f, float waitTime = 0f)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float startVal = canvasGroup.alpha;
        float time = 0;

        while (time < dur)
        {
            canvasGroup.alpha = Mathf.Lerp(startVal, 1, time / dur);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    /// <summary>
    /// Go to the screen after the Score screen. This can be the Save Score screen, or the Post Score screen with Restart and Main Menu options.
    /// </summary>
    public void PostScoreScreen()
    {
        scoreScreen.SetActive(false);
        if (newScore)
        {
            saveScoreScreen.SetActive(true);
        } else
        {
            postScoreScreen.SetActive(true);
        }
    }

    public void SaveScore()
    {
        HighScoreManager.current.SaveScore(playerName.text);
    }

    public void ScoreScreen()
    {
        Debug.Log("Showing score screen.");
        scoreScreenOpen = true;
        gameOverUI.SetActive(false);
        scoreScreen.SetActive(true);
        CalculateFinalScore();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
