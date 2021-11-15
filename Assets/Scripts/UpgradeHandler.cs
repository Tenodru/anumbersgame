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
    public float hpGainPerLevel;
    [Header("Fuel")]
    public int fuelGainLevel;
    public float fuelGainPerLevel;

    [Header("Other")]
    public bool canUpgrade = false;
    public bool upgradeScreenOpen = false;

    // Coroutine variables.
    Coroutine upgradeIndicatorCo;
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
                FadeObjectCycle(UpgradeReferences.current.xpGainButton, xpCo);
                FadeObjectCycle(UpgradeReferences.current.hpGainButton, hpCo);
                FadeObjectCycle(UpgradeReferences.current.fuelGainButton, fuelCo);
            }
            else
            {
                references.xpGainButton.SetActive(false);
                references.hpGainButton.SetActive(false);
                references.fuelGainButton.SetActive(false);
                if (xpCo != null)
                    StopCoroutine(xpCo);
                if (hpCo != null)
                    StopCoroutine(hpCo);
                if (fuelCo != null)
                    StopCoroutine(fuelCo);
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
        }
    }

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

    public void OpenUpgradeScreen()
    {
        references.upgradeUICanvas.SetActive(true);
        references.mainCanvas.SetActive(false);
        upgradeScreenOpen = true;
    }

    public void CloseUpgradeScreen()
    {
        upgradeScreenOpen = false;
        references.upgradeUICanvas.SetActive(false);
        references.mainCanvas.SetActive(true);
    }
}
