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
        LoadScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Load scores at the start of the game, and populate high scores table.
    /// </summary>
    public void LoadScores()
    {
        ReadScores();
        if (scores == null)
        {
            scores = new ScoresList(new List<ScoreEntry>());
            return;
        }
        
        for (int i = 0; i < scores.list.Count; i++)
        {
            ScoreDisplayHandler.current.scoreDisplays[i].rank.gameObject.SetActive(true);
            ScoreDisplayHandler.current.scoreDisplays[i].name.gameObject.SetActive(true);
            ScoreDisplayHandler.current.scoreDisplays[i].score.gameObject.SetActive(true);

            ScoreDisplayHandler.current.scoreDisplays[i].rank.text = (i + 1).ToString();
            ScoreDisplayHandler.current.scoreDisplays[i].name.text = scores.list[i].name;
            ScoreDisplayHandler.current.scoreDisplays[i].score.text = scores.list[i].score.ToString();
        }
    }

    /// <summary>
    /// Read scores from PlayerPrefs and load into scores object.
    /// </summary>
    public void ReadScores()
    {
        string scoresListOld = PlayerPrefs.GetString("scoresList");
        Debug.Log("Scores: " + scoresListOld);
        scores = JsonUtility.FromJson<ScoresList>(scoresListOld);
    }

    /// <summary>
    /// Save a new score to the scores list.
    /// </summary>
    public void SaveScore(string name)
    {
        ScoreEntry newScore = new ScoreEntry(name, GameStateHandler.current.fastScore);

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
        Debug.Log("Score of " + GameStateHandler.current.fastScore + " saved.");
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
