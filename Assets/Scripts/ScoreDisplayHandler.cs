using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayHandler : MonoBehaviour
{
    public static ScoreDisplayHandler current;

    [Header("Score Display Elements")]
    public List<ScoreEntryDisplay> scoreDisplays;

    private void Awake()
    {
        current = this;
    }

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
    [Tooltip("The player position/rank display.")]
    public TextMeshProUGUI rank;
    [Tooltip("The player name display.")]
    public TextMeshProUGUI name;
    [Tooltip("The player score display.")]
    public TextMeshProUGUI score;
}
