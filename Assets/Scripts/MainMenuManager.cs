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

    [Header("ExitGame Canvas References")]
    public GameObject exitGameConfirmScreen;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuScreen.SetActive(true);
        exitGameConfirmScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Starts a new game.
    /// </summary>
    public void StartGame()
    {
        GameManager.current.StartTime();
        SceneManager.LoadScene("Arena");
    }

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
    }
}
