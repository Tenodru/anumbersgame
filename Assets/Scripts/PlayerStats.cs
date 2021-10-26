using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles experience and other things.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Experience and Levels")]
    public int xpGainMultiplier = 1;
    public int startingLevel = 1;

    // Other variables and references.
    StatsDisplay statDisplay;
    public int currentXP;
    public int currentXPTotal;
    public int currentLevel;
    public bool canGainXP = true;

    // Start is called before the first frame update
    void Start()
    {
        statDisplay = GetComponent<StatsDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when the player earns XP.
    /// </summary>
    /// <param name="xpAmount"></param>
    public void GainXP(int xpAmount)
    {
        int reqXP = GetReqXPForLevel();
        Debug.Log("Gained XP: " + xpAmount);
        if (currentXP + (xpAmount * xpGainMultiplier) >= reqXP)
        {
            int extraXP = (currentXP + (xpAmount * xpGainMultiplier)) - reqXP;
            Debug.Log("Extra XP: " + extraXP);
            GainLevel();
            statDisplay.ResetXPBar();
            GainXP(extraXP);
            return;
        }
        if (!canGainXP)
        {
            return;
        }
        currentXP += (xpAmount * xpGainMultiplier);
        statDisplay.ChangeXPDisplay((xpAmount * xpGainMultiplier), currentLevel);
        // Do other regular xp gain stuff.
        
    }

    /// <summary>
    /// Gets the total required amount of XP for the specified level. If no level is specified, uses the current level.
    /// </summary>
    /// <param name="curLevel"></param>
    /// <returns></returns>
    public int GetReqXPForLevel(int curLevel = 0)
    {
        if (curLevel == 0)
            curLevel = currentLevel;
        //return 10 * (int)Mathf.Pow(curLevel, 2);
        return 10;
    }

    /// <summary>
    /// Increases the player's level.
    /// </summary>
    public void GainLevel()
    {
        currentLevel++;
        currentXP = 0;
        // Do other level-up stuff.
    }
}
