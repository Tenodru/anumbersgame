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
        percentage = (float) amount / (float) weaponControl.maxFuel;
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
    public virtual void ChangeXPDisplay(int amount, int curLevel)
    {
        percentage = (float)amount / (float)playerStats.GetReqXPForLevel();
        Debug.Log("Percentage:" + percentage);

        if (orientationXP == BarOrientation.Vertical)
        {
            barChange = percentage * xpDisplay.barDefaultHeightSize;
            xpDisplay.UpdateBar(BarAttribute.Height, barChange, curLevel);
        }
        if (orientationXP == BarOrientation.Horizontal)
        {
            barChange = percentage * xpDisplay.barDefaultWidthSize;
            Debug.Log("Default Bar Width: " + xpDisplay.barDefaultWidthSize);
            xpDisplay.UpdateBar(BarAttribute.Width, barChange, curLevel);
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
        playerStats = GetComponent<PlayerStats>();
        fuelDisplay = new DisplayElementBar(fuelDisplay.bar, fuelDisplay.label, fuelDisplay.displayText, fuelDisplay.background);
        xpDisplay = new DisplayElementBar(xpDisplay.bar, xpDisplay.label, xpDisplay.displayText, xpDisplay.background);
        xpDisplay.bar.sizeDelta = new Vector2(0, xpDisplay.bar.sizeDelta.y);
    }
}

