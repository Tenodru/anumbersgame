using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [Header("XP")]
    public int xpGainLevel;
    public float xpGainPerLevel;
    [Header("HP")]
    public int hpGainLevel;
    public float hpGainPerLevel = 20;
    [Header("Fuel")]
    public int fuelGainLevel;
    public float fuelGainPerLevel;

    [Header("Other")]
    public bool canUpgrade = false;
    public bool upgradeScreenOpen = false;

    // Coroutine variables.
    Coroutine upgradeIndicatorCo;
    Coroutine upgradeScreenIndicatorCo;
    Coroutine xpCo;
    Coroutine hpCo;
    Coroutine fuelCo;

    // Other references.
    public static UpgradeHandler current;
    PlayerStats stats;
    UpgradeReferences references;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.current;
        references = UpgradeReferences.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.upgradePoints > 0)
        {
            canUpgrade = true;
            references.upgradePointsIndicator.SetActive(true);
            references.upgradePointsIndicatorText.text = stats.upgradePoints.ToString();
            FadeObjectCycle(UpgradeReferences.current.upgradePointsIndicator, upgradeIndicatorCo);
        }
        else
        {
            if (upgradeIndicatorCo != null)
                StopCoroutine(upgradeIndicatorCo);
            references.upgradePointsIndicator.SetActive(false);
        }

        if (upgradeScreenOpen)
        {
            if (stats.upgradePoints > 0)
            {
                references.xpGainButton.SetActive(true);
                references.hpGainButton.SetActive(true);
                references.fuelGainButton.SetActive(true);
                references.upgradeScreenPointsIndicator.SetActive(true);
                references.upgradeScreenPointsIndicatorText.text = stats.upgradePoints.ToString();
                FadeObjectCycle(UpgradeReferences.current.upgradeScreenPointsIndicator, upgradeScreenIndicatorCo);
                FadeObjectCycle(UpgradeReferences.current.xpGainButton, xpCo);
                FadeObjectCycle(UpgradeReferences.current.hpGainButton, hpCo);
                FadeObjectCycle(UpgradeReferences.current.fuelGainButton, fuelCo);
            }
            else
            {
                references.xpGainButton.SetActive(false);
                references.hpGainButton.SetActive(false);
                references.fuelGainButton.SetActive(false);
                references.upgradeScreenPointsIndicator.SetActive(false);
                if (xpCo != null)
                    StopCoroutine(xpCo);
                if (hpCo != null)
                    StopCoroutine(hpCo);
                if (fuelCo != null)
                    StopCoroutine(fuelCo);
                if (upgradeScreenIndicatorCo != null)
                    StopCoroutine(upgradeScreenIndicatorCo);
            }
        }
        else
        {
            if (xpCo != null)
                StopCoroutine(xpCo);
            if (hpCo != null)
                StopCoroutine(hpCo);
            if (fuelCo != null)
                StopCoroutine(fuelCo);
            if (upgradeScreenIndicatorCo != null)
                StopCoroutine(upgradeScreenIndicatorCo);
        }
    }

    /// <summary>
    /// Starts a fade-out/fade-in cycle for the specified object.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="co"></param>
    public void FadeObjectCycle(GameObject obj, Coroutine co)
    {
        co = StartCoroutine(FadeObjectOut(obj, 1, co));
    }

    /// <summary>
    /// Fades the object alpha out to 0 over time.
    /// </summary>
    /// <param name="dur"></param>
    /// <returns></returns>
    IEnumerator FadeObjectOut(GameObject obj, float dur, Coroutine co = null)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float startVal = canvasGroup.alpha;
        float time = 0;

        while (time < dur)
        {
            canvasGroup.alpha = Mathf.Lerp(startVal, 0, time / dur);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        if (co != null)
            StartCoroutine(FadeObjectIn(obj, dur, co));
        else
            StartCoroutine(FadeObjectIn(obj, dur));
    }

    /// <summary>
    /// Fades the object alpha in to 1 over time.
    /// </summary>
    /// <param name="dur"></param>
    /// <returns></returns>
    IEnumerator FadeObjectIn(GameObject obj, float dur, Coroutine co = null)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float startVal = canvasGroup.alpha;
        float time = 0;

        while (time < dur)
        {
            canvasGroup.alpha = Mathf.Lerp(startVal, 1, time / dur);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
        if (co != null)
            StartCoroutine(FadeObjectOut(obj, dur, co));
        else
            StartCoroutine(FadeObjectOut(obj, dur));
    }

    /// <summary>
    /// Opens the upgrade screen.
    /// </summary>
    public void OpenUpgradeScreen()
    {
        references.upgradeUICanvas.SetActive(true);
        references.mainCanvas.SetActive(false);
        upgradeScreenOpen = true;
    }

    /// <summary>
    /// Closes the upgrade screen.
    /// </summary>
    public void CloseUpgradeScreen()
    {
        upgradeScreenOpen = false;
        references.upgradeUICanvas.SetActive(false);
        references.mainCanvas.SetActive(true);
    }

    // Base Upgrades -------------------------------------------------------------------------------------------

    public void UpgradeHP()
    {
        hpGainLevel += 1;
        stats.IncreaseBaseHealthPercentage(hpGainPerLevel);
        references.hpLevel.text = hpGainLevel.ToString();
        stats.upgradePoints--;
    }

    public void UpgradeXP()
    {
        xpGainLevel += 1;
        stats.IncreaseXPGain(xpGainPerLevel);
        references.xpLevel.text = xpGainLevel.ToString();
        stats.upgradePoints--;
    }

    public void UpgradeFuel()
    {
        fuelGainLevel += 1;
        stats.IncreaseMaxFuelPercentage(fuelGainPerLevel);
        references.fuelLevel.text = fuelGainLevel.ToString();
        stats.upgradePoints--;
    }

}
