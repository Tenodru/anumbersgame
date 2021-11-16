using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AscentUI.Elements;
using System;

public class StatsDisplay : MonoBehaviour
{
    [Header("FuelDisplay")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientationFuel;
    [Tooltip("This character's fuel display element.")]
    public DisplayElementBar fuelDisplay;
    [Tooltip("This character's fuel bar.")]
    public Slider fuelBar;
    [Tooltip("This character's fuel level label.")]
    public TextMeshProUGUI fuelLevelLabel;

    [Header("XP Display")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientationXP;
    [Tooltip("This character's xp display element.")]
    public DisplayElementBar xpDisplay;
    [Tooltip("This character's XP bar.")]
    public Slider xpDisplayBar;
    [Tooltip("This character's currentLevel label.")]
    public TextMeshProUGUI currentLevelLabel;
    [Tooltip("This character's level progress label.")]
    public TextMeshProUGUI xpProgressLabel;

    [Header("Health Display")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientationHP;
    [Tooltip("This character's health display element.")]
    public DisplayElementBar hpDisplay;
    [Tooltip("This character's HP bar.")]
    public Slider healthDisplayBar;
    [Tooltip("This character's currentHP label.")]
    public TextMeshProUGUI currentHealthLabel;
    [Tooltip("This character's maxHP label.")]
    public TextMeshProUGUI maxHealthLabel;
    [Tooltip("The max width of this character's HP bar.")]
    public float healthBarMaxWidth;

    float healthBarWidth;
    float curMaxHealth;

    float percentage;
    float barChange;

    PlayerWeaponController weaponControl;
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Fuel -------------------------------------------------------------------------------------------

    public virtual void UpdateFuelBar(float barValue)
    {
        Debug.Log("fuel value: " + barValue);
        fuelBar.value = barValue;
        fuelLevelLabel.text = playerStats.fuel.ToString();
    }

    /// <summary>
    /// Updates the fuel display.
    /// </summary>
    /// <param name="amount"></param>
    [System.Obsolete("Deprecated. Use UpdateXPBar with Slider instead.")]
    public virtual void ChangeFuelDisplay(int amount, int curFuel)
    {
        percentage = (float) amount / (float) playerStats.maxFuel;
        //Debug.Log("Percentage:" + percentage);

        if (orientationFuel == BarOrientation.Vertical)
        {
            barChange = percentage * fuelDisplay.barDefaultHeightSize;
            fuelDisplay.UpdateBar(BarAttribute.Height, barChange, curFuel);
        }
        if (orientationFuel == BarOrientation.Horizontal)
        {
            barChange = percentage * fuelDisplay.barDefaultWidthSize;
            fuelDisplay.UpdateBar(BarAttribute.Width, barChange, curFuel);
        }
    }

    // XP and Levels -------------------------------------------------------------------------------------------

    public virtual void UpdateXPBar(float barValue)
    {
        xpDisplayBar.value = barValue;
        currentLevelLabel.text = "Level: " + playerStats.GetCurrentLevel().ToString();
        xpProgressLabel.text = (Mathf.Round(playerStats.GetLevelProgress(true) * 100) / 100).ToString() + "%";
    }

    /// <summary>
    /// Updates the XP display.
    /// </summary>
    /// <param name="amount"></param>
    [System.Obsolete("Deprecated. Use UpdateXPBar with Slider instead.")]
    public virtual void ChangeXPDisplay(float amount, int curLevel)
    {
        percentage = (float)amount / (float)playerStats.GetReqXPForLevel();

        if (orientationXP == BarOrientation.Vertical)
        {
            barChange = percentage * xpDisplay.barDefaultHeightSize;
            xpDisplay.UpdateBar(BarAttribute.Height, barChange, curLevel);
        }
        if (orientationXP == BarOrientation.Horizontal)
        {
            barChange = percentage * xpDisplay.barDefaultWidthSize;
            xpDisplay.UpdateBar(BarAttribute.Width, barChange, curLevel);
        }
    }

    // Health -------------------------------------------------------------------------------------------

    public virtual void UpdateHealthBar(float barValue)
    {
        healthDisplayBar.value = barValue;
        currentHealthLabel.text = playerStats.GetCurrentHealth().ToString();
    }

    public virtual void UpdateMaxHealth()
    {
        ResizeHealthBar();
        UpdateMaxHealthLabel();
    }

    public virtual void ResizeHealthBar()
    {
        healthBarWidth = healthDisplayBar.GetComponent<RectTransform>().sizeDelta.x;
        float percentChange = (Mathf.Abs(playerStats.GetMaxHealth() - curMaxHealth) / curMaxHealth) / 2.0f;
        float flatChange = percentChange * healthBarWidth;
        Debug.Log("curMaxHealth: " + curMaxHealth);
        Debug.Log("Percent Change: " + percentChange);
        Debug.Log("Flat Change: " + flatChange);

        // If the width increase would bring the bar past max, resize up to max.
        if (healthBarWidth + flatChange > healthBarMaxWidth)
        {
            healthBarWidth = healthBarMaxWidth;
            healthDisplayBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarWidth, healthDisplayBar.GetComponent<RectTransform>().sizeDelta.y);
        }
        else
        {
            healthBarWidth += flatChange;
            healthDisplayBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarWidth, healthDisplayBar.GetComponent<RectTransform>().sizeDelta.y);
        }

        curMaxHealth = playerStats.GetMaxHealth();
    }

    public virtual void UpdateMaxHealthLabel()
    {
        maxHealthLabel.text = "/ " + playerStats.GetMaxHealth().ToString();
    }

    /// <summary>
    /// Updates the Health display.
    /// </summary>
    /// <param name="amount"></param>
    [System.Obsolete("Deprecated. Use UpdateHealthBar with Slider instead.")]
    public virtual void ChangeHealthDisplay(float amount, float curHealth)
    {
        percentage = (float)amount / (float)playerStats.GetMaxHealth();
        Debug.Log("Percentage:" + percentage);

        if (orientationXP == BarOrientation.Vertical)
        {
            barChange = percentage * hpDisplay.barDefaultHeightSize;
            hpDisplay.UpdateBar(BarAttribute.Height, barChange, curHealth);
        }
        if (orientationXP == BarOrientation.Horizontal)
        {
            barChange = percentage * hpDisplay.barDefaultWidthSize;
            Debug.Log("Default Bar Width: " + hpDisplay.barDefaultWidthSize);
            hpDisplay.UpdateBar(BarAttribute.Width, barChange, curHealth);
        }
    }

    public void ResetXPBar ()
    {
        xpDisplay.bar.sizeDelta = new Vector2(0, xpDisplay.bar.sizeDelta.y);
    }

    /// <summary>
    /// Code called on start.
    /// </summary>
    public virtual void OnStart()
    {
        weaponControl = GetComponent<PlayerWeaponController>();
        playerStats = PlayerStats.current;

        healthBarWidth = healthDisplayBar.GetComponent<RectTransform>().sizeDelta.x;
        curMaxHealth = playerStats.GetMaxHealth();
    }
}

