using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AscentUI.Elements;
using System;

public class StatsDisplay : MonoBehaviour
{
    [Header("FuelDisplay")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientationFuel;
    [Tooltip("This character's fuel display element.")]
    public DisplayElementBar fuelDisplay;

    [Header("XP Display")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientationXP;
    [Tooltip("This character's xp display element.")]
    public DisplayElementBar xpDisplay;

    [Header("Health Display")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientationHP;
    [Tooltip("This character's health display element.")]
    public DisplayElementBar hpDisplay;

    float percentage;
    float barChange;

    PlayerWeaponController weaponControl;
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    /// <summary>
    /// Updates the fuel display.
    /// </summary>
    /// <param name="amount"></param>
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

    /// <summary>
    /// Updates the XP display.
    /// </summary>
    /// <param name="amount"></param>
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

    /// <summary>
    /// Updates the Health display.
    /// </summary>
    /// <param name="amount"></param>
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
        fuelDisplay = new DisplayElementBar(fuelDisplay.bar, fuelDisplay.label, fuelDisplay.displayText, fuelDisplay.background);
        xpDisplay = new DisplayElementBar(xpDisplay.bar, xpDisplay.label, xpDisplay.displayText, xpDisplay.background);
        ResetXPBar();
        hpDisplay = new DisplayElementBar(hpDisplay.bar, hpDisplay.label, hpDisplay.displayText, hpDisplay.background);
    }
}

