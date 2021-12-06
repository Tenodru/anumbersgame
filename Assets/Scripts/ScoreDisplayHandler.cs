using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayHandler : MonoBehaviour
{
    [Header("Score Display Elements")]
    public ScoreEntryDisplay score1;
    public ScoreEntryDisplay score2;
    public ScoreEntryDisplay score3;
    public ScoreEntryDisplay score4;
    public ScoreEntryDisplay score5;
    public ScoreEntryDisplay score6;
    public ScoreEntryDisplay score7;
    public ScoreEntryDisplay score8;
    public ScoreEntryDisplay score9;
    public ScoreEntryDisplay score10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class ScoreEntryDisplay
{
    [Tooltip("The element id. Corresponds to index in list.")]
    public int id = 0;
    [Tooltip("The player position/rank display.")]
    public TextMeshProUGUI rank;
    [Tooltip("The player name display.")]
    public TextMeshProUGUI name;
    [Tooltip("The player score display.")]
    public TextMeshProUGUI score;
}
