using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles experience and other things.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Experience and Levels")]
    public float xpGainMultiplier = 1;
    public int startingLevel = 1;
    public bool canGainXP = true;
    public int upgradePoints = 0;

    [Header("Health")]
    public float startingHealth = 100;
    float currentHealth;
    float maxHealth;

    [Header("Fuel")]
    public int startingFuel = 100;
    public int maxFuel = 100;
    public int baseFuelConsumption = 10;
    public int fuelRefillMultiplier = 1;
    public float fuelRefillDelay = 1;

    // Other variables and references.
    public static PlayerStats current;
    StatsDisplay statDisplay;
    float currentXP;
    float currentXPTotal;
    int currentLevel;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        statDisplay = GetComponent<StatsDisplay>();
        maxHealth = startingHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // XP and Levels -------------------------------------------------------------------------------------------

    /// <summary>
    /// Called when the player earns XP.
    /// </summary>
    /// <param name="xpAmount"></param>
    public void GainXP(float xpAmount)
    {
        float reqXP = GetReqXPForLevel();
        Debug.Log("Gained XP: " + xpAmount);
        if (currentXP + (xpAmount * xpGainMultiplier) >= reqXP)
        {
            float extraXP = (currentXP + (xpAmount * xpGainMultiplier)) - reqXP;
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
    public float GetReqXPForLevel(int curLevel = 0)
    {
        if (curLevel == 0)
            curLevel = currentLevel;
        return 10 * Mathf.Pow(curLevel, 2);
    }

    /// <summary>
    /// Increases the player's level.
    /// </summary>
    public void GainLevel()
    {
        currentLevel++;
        currentXP = 0;
        upgradePoints++;
        // Do other level-up stuff.
    }

    /// <summary>
    /// Gets this Player's current XP.
    /// </summary>
    /// <returns>float | currentXP</returns>
    public float GetCurrentXP()
    {
        return currentXP;
    }

    /// <summary>
    /// Gets this Player's current XP total.
    /// </summary>
    /// <returns>int | currentXPTotal</returns>
    public float GetCurrentXPTotal()
    {
        return currentXPTotal;
    }

    /// <summary>
    /// Gets this Player's current level.
    /// </summary>
    /// <returns>int | currentXP</returns>
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Health -------------------------------------------------------------------------------------------

    /// <summary>
    /// Player takes damage. Health is decreased by the specified amount.
    /// </summary>
    /// <param name="damage">The amount to decrease player health by.</param>
    public void TakeDamage (float damage)
    {
        if (currentHealth - damage < 0)
        {
            statDisplay.ChangeHealthDisplay(-currentHealth, 0);
            currentHealth = 0;
            return;
        }
        currentHealth -= damage;
        statDisplay.ChangeHealthDisplay(-damage, currentHealth);
        Debug.Log("Player Health: " + currentHealth);
    }

    /// <summary>
    /// Get this Player's starting health.
    /// </summary>
    /// <returns></returns>
    public float GetStartingHealth()
    {
        return startingHealth;
    }

    /// <summary>
    /// Get this Player's current health.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Get this Player's maximum health.
    /// </summary>
    /// <returns></returns>
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
