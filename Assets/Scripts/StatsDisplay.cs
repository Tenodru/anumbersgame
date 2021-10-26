using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AscentUI.Elements;
using System;

public class StatsDisplay : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientation;

    [Header("UI/Display References")]
    [Tooltip("This character's fuel display element.")]
    public DisplayElementBar fuelDisplay;

    float percentage;
    float barChange;

    PlayerWeaponController weaponControl;

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

        if (orientation == BarOrientation.Vertical)
        {
            barChange = percentage * fuelDisplay.barDefaultHeightSize;
            fuelDisplay.UpdateBar(BarAttribute.Height, barChange, curFuel);
        }
        if (orientation == BarOrientation.Horizontal)
        {
            barChange = percentage * fuelDisplay.barDefaultWidthSize;
            fuelDisplay.UpdateBar(BarAttribute.Width, barChange, curFuel);
        }
    }

    /// <summary>
    /// Code called on start.
    /// </summary>
    public virtual void OnStart()
    {
        weaponControl = GetComponent<PlayerWeaponController>();
        fuelDisplay = new DisplayElementBar(fuelDisplay.bar, fuelDisplay.label, fuelDisplay.displayText, fuelDisplay.background);
    }
}

