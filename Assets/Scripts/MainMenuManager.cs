using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu interactions.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
