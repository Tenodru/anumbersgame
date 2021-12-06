using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager current;

    [Header("Enter Name UI References")]
    public InputField playerName;

    public static ScoresList scores;

    private void Awake()
    {
        current = this;
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

    public void SaveScore()
    {
        ScoreEntry playerScore = new ScoreEntry(playerName.text, GameStateHandler.current.playerScore);
        scores.list.Add(playerScore);
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
