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
    float moveTowardsDur = 1000f;

    bool addingScore = false;
    bool gameEnded = false;
    bool scoreScreenOpen = false;

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

    public void CalculateFinalScore()
    {
        displayScore = playerScore;
        scoreText.text = displayScore.ToString();
        GameManager.current.scoreModifiers[0].score = currentTime * scoreMultiplier;
        StartCoroutine(ShowScoreModifiers(GameManager.current.scoreModifiers, 0f));
        StartCoroutine(FadeObjectIn(scoreModifier.gameObject));
    }

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

    IEnumerator FadeInScoreModifier(float time = 2f)
    {
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("Fading in.");
        scoreModifier.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    IEnumerator FadeOutScoreModifier(float time = 2f)
    {
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("Fading out.");
        scoreModifier.GetComponent<Animator>().SetTrigger("FadeOut");
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
