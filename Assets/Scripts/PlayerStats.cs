using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles experience and other things.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Experience and Levels")]
    public float startingXPGainMultiplier = 1;
    public float xpGainMultiplier = 1;
    public float xpGainBonus = 0.0f;
    public int startingLevel = 1;
    public bool canGainXP = true;
    public int upgradePoints = 0;

    [Header("Health")]
    public float startingHealth = 100;
    public float healthBonus = 0.0f;
    float currentHealth;
    float maxHealth;

    [Header("Fuel")]
    public float startingFuel = 100;
    public float maxFuel = 100;
    public float fuel;
    public float fuelBonus = 0.0f;
    public float baseFuelConsumption = 10;
    public float fuelRefillMultiplier = 1;
    public float fuelRefillDelay = 1;

    [Header("Weapon")]
    public float startingProjSpeed = 6;
    public float projSpeed = 6;
    public float startingDamage = 1;
    public float damage = 1;
    public float damageBonus = 0.0f;

    // Other variables and references.
    public static PlayerStats current;
    StatsDisplay statDisplay;
    
    float currentXP;
    float currentXPTotal;
    int currentLevel;

    private void Awake()
    {
        current = this;
        maxHealth = startingHealth;
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        statDisplay = GetComponent<StatsDisplay>();
        xpGainMultiplier = startingXPGainMultiplier;
        fuel = startingFuel;
        projSpeed = startingProjSpeed;
        damage = startingDamage;
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
        float gainAmount = (xpAmount * xpGainMultiplier);
        if (currentXP + (gainAmount) >= reqXP)
        {
            float extraXP = (currentXP + (xpAmount * xpGainMultiplier)) - reqXP;
            GainLevel();
            statDisplay.UpdateXPBar(0);
            currentXP += (extraXP);
            statDisplay.UpdateXPBar((currentXP / reqXP) * 100);
            statDisplay.GainXPIndicator(gainAmount);
            return;
        }
        if (!canGainXP)
        {
            return;
        }
        currentXP += (gainAmount);
        statDisplay.UpdateXPBar((currentXP / reqXP) * 100);
        statDisplay.GainXPIndicator(gainAmount);
        //statDisplay.ChangeXPDisplay((xpAmount * xpGainMultiplier), currentLevel);
        // Do other regular xp gain stuff.

    }

    /// <summary>
    /// Gets the total required amount of XP for the specified level. If no level is specified, uses the next level.
    /// </summary>
    /// <param name="curLevel"></param>
    /// <returns></returns>
    public float GetReqXPForLevel(int curLevel = 0)
    {
        if (curLevel == 0)
            curLevel = currentLevel + 1;
        //return 10 * Mathf.Pow(curLevel, 2);
        return 10 * Mathf.Pow((1 + 0.2f), curLevel);
    }

    /// <summary>
    /// Gets the current progress or percentage towards the next level.
    /// </summary>
    /// <param name="asPercent">If true, returns progress as a percent. False by default.</param>
    /// <returns></returns>
    public float GetLevelProgress(bool asPercent = false)
    {
        if (asPercent)
        {
            return (currentXP / GetReqXPForLevel() * 100);
        }
        return currentXP / GetReqXPForLevel();
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
    /// Increases XP gain by the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage by which base XP gain should be increased.</param>
    public void IncreaseXPGain(float percentage)
    {
        xpGainBonus += percentage;
        xpGainMultiplier = startingXPGainMultiplier * (1 + xpGainBonus);
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
        if (currentHealth - damage <= 0)
        {
            //statDisplay.ChangeHealthDisplay(-currentHealth, 0);
            statDisplay.UpdateHealthBar(0);
            currentHealth = 0;
            return;
        }
        currentHealth -= damage;
        //statDisplay.ChangeHealthDisplay(-damage, currentHealth);
        statDisplay.UpdateHealthBar((currentHealth / maxHealth) * 100);
    }

    /// <summary>
    /// Player heals for the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage of max health the player should heal by.</param>
    public void GainHealthPercentage(float percentage)
    {
        float amount = maxHealth * percentage;
        GainHealthFlat(amount);
    }

    /// <summary>
    /// Player gains flat health. Health is increased by the specified amount.
    /// </summary>
    /// <param name="amount">The amount to increase player health by.</param>
    public void GainHealthFlat(float amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            //statDisplay.ChangeHealthDisplay((maxHealth - amount), maxHealth);
            statDisplay.UpdateHealthBar(100);
            currentHealth = maxHealth;
            return;
        }
        currentHealth += amount;
        //statDisplay.ChangeHealthDisplay(amount, currentHealth);
        statDisplay.UpdateHealthBar((currentHealth / maxHealth) * 100);
    }

    /// <summary>
    /// Increases player maximum health by a flat amount.
    /// </summary>
    /// <param name="amount">The amount to increase maxHealth by.</param>
    /// <param name="heal">Whether player should be healed for the same amount. True by default.</param>
    public void IncreaseMaxHealthFlat (float amount, bool heal = true)
    {
        maxHealth += amount;
        if (heal)
        {
            GainHealthFlat(amount);
        }
    }

    /// <summary>
    /// Increases player base health by the specified percentage.
    /// </summary>
    /// <param name="percentage">The amount to increase base health by.</param>
    /// <param name="heal">Whether player should be healed for the same amount. True by default.</param>
    public void IncreaseBaseHealthPercentage(float percentage, bool heal = true)
    {
        healthBonus += percentage;
        float amount = maxHealth;
        maxHealth = startingHealth * (1 + healthBonus);
        amount = maxHealth - amount;
        statDisplay.UpdateMaxHealth();

        if (heal)
        {
            GainHealthFlat(amount);
        }
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

    // Fuel -------------------------------------------------------------------------------------------

    /// <summary>
    /// Player gains Fuel. Fuel is increased by the specified amount.
    /// </summary>
    /// <param name="amount">The amount to increase player fuel by.</param>
    public void GainFuel(float amount)
    {
        if (fuel + amount > maxFuel)
        {
            //statDisplay.ChangeFuelDisplay((maxFuel - amount), maxFuel);
            statDisplay.UpdateFuelBar(100);
            fuel = maxFuel;
            return;
        }
        fuel += amount;
        //statDisplay.ChangeFuelDisplay(amount, fuel);
        statDisplay.UpdateFuelBar((fuel / maxFuel) * 100);
    }

    /// <summary>
    /// Increases player maximum fuel.
    /// </summary>
    /// <param name="amount">The amount to increase maxFuel by.</param>
    /// <param name="replenish">Whether player fuel should be replenished for the same amount. True by default.</param>
    public void IncreaseMaxFuel(float amount, bool replenish = true)
    {
        maxFuel += amount;
        if (replenish)
        {
            GainFuel(amount);
        }
    }

    /// <summary>
    /// Increases player base max fuel by the specified percentagge.
    /// </summary>
    /// <param name="percentage"></param>
    /// <param name="replenish"></param>
    public void IncreaseMaxFuelPercentage(float percentage, bool replenish = true)
    {
        fuelBonus += percentage;
        float amount = maxFuel;
        maxFuel = startingFuel * (1 + fuelBonus);
        amount = maxFuel - amount;

        if (replenish)
        {
            GainFuel(amount);
        }
    }
}
