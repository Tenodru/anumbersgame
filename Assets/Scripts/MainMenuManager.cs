using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu interactions.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("MainMenu Canvas References")]
    public GameObject mainMenuScreen;

    [Header("HighScores Canvas References")]
    public GameObject highScoresScreen;

    [Header("ExitGame Canvas References")]
    public GameObject exitGameConfirmScreen;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuScreen.SetActive(true);
        exitGameConfirmScreen.SetActive(false);
        highScoresScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sends the player to the high scores screen.
    /// </summary>
    public void HighScores()
    {
        mainMenuScreen.SetActive(false);
        exitGameConfirmScreen.SetActive(false);
        highScoresScreen.SetActive(true);
        HighScoreManager.current.LoadScores();
    }

    /// <summary>
    /// Starts a new game.
    /// </summary>
    public void StartGame()
    {
        GameManager.current.StartTime();
        SceneManager.LoadScene("Arena");
    }

    /// <summary>
    /// Sends the player to the quit confirmation screen.
    /// </summary>
    public void ExitGame()
    {
        mainMenuScreen.SetActive(false);
        exitGameConfirmScreen.SetActive(true);
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        mainMenuScreen.SetActive(true);
        exitGameConfirmScreen.SetActive(false);
        highScoresScreen.SetActive(false);
    }
}
