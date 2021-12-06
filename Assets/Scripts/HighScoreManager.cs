using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager current;

    [Header("Enter Name UI References")]
    public InputField playerName;

    [Header("Score Options")]
    public int maxScores = 10;

    public static ScoresList scores;

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

    // Start is called before the first frame update
    void Start()
    {
        ReadScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadScores()
    {
        string scoresListOld = PlayerPrefs.GetString("scoresList");
        scores = JsonUtility.FromJson<ScoresList>(scoresListOld);
    }

    /// <summary>
    /// Save a new score to the scores list.
    /// </summary>
    public void SaveScore(string name)
    {
        ScoreEntry newScore = new ScoreEntry(playerName.text, GameStateHandler.current.fastScore);

        // If score limit has been reached, remove the lowest score, then add the new score and sort.
        if (scores.list.Count >= maxScores)
        {
            scores.list.RemoveAt(scores.list.Count - 1);
            scores.list.Add(newScore);
            scores.list.Sort();
        } else
        {
            scores.list.Add(newScore);
        }
        
        string json = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString("scoresList", json);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class ScoresList
{
    public List<ScoreEntry> list;

    public ScoresList (List<ScoreEntry> l)
    {
        list = l;
    }
}

[System.Serializable]
public class ScoreEntry
{
    [Tooltip("The player name.")]
    public string name;
    [Tooltip("The player score.")]
    public int score;

    public ScoreEntry(string n, int s)
    {
        name = n;
        score = s;
    }
}

[System.Serializable]
public class ScoreEntryDisplay
{
    [Tooltip("The player position/rank display.")]
    public TextMeshProUGUI rank;
    [Tooltip("The player name display.")]
    public TextMeshProUGUI name;
    [Tooltip("The player score display.")]
    public TextMeshProUGUI score;
}
