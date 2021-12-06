using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles global stuff like tracking time.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public int difficulty = 1;

    public List<ScoreModifier> scoreModifiers;

    [Tooltip("Time elapsed during start screen.")]
    public float elapsedTime = 0;
    [Tooltip("Time elapsed during previous games.")]
    public float elapsedTimeGame = 0;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartTime()
    {
        elapsedTime = Time.time;
        scoreModifiers.Add(new ScoreModifier("Time"));
        scoreModifiers.Add(new ScoreModifier("Difficulty", 2000));
    }
}

[System.Serializable]
public class ScoreModifier
{
    public string name;
    public int score;

    public ScoreModifier(string n, int s = 0)
    {
        name = n;
        score = s;
    }
}
