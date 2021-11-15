using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Reference class for all Upgrade gameObjects.
/// </summary>
public class UpgradeReferences : MonoBehaviour
{
    public static UpgradeReferences current;

    [Header("Upgrade Point Counters")]
    public GameObject upgradePointsIndicator;
    public TextMeshProUGUI upgradePointsIndicatorText;
    public GameObject upgradeScreenPointsIndicator;
    public TextMeshProUGUI upgradeScreenPointsIndicatorText;

    [Header("Upgrade Indicators")]
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
