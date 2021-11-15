using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reference class for all Upgrade gameObjects.
/// </summary>
public class UpgradeReferences : MonoBehaviour
{
    public static UpgradeReferences current;

    [Header("References")]
    public GameObject upgradePointsIndicator;
    public GameObject xpGainButton;
    public GameObject hpGainButton;
    public GameObject fuelGainButton;

    [Header("Canvas References")]
    public GameObject mainCanvas;
    public GameObject upgradeUICanvas; 

    private void Awake()
    {
        current = this;
    }
}
