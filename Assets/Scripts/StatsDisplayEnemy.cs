using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Stats display handler for enemy characters.
/// </summary>
public class StatsDisplayEnemy : MonoBehaviour
{
    [Header("Health Display")]
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

    EnemyBehavior stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyBehavior>();

        healthBarWidth = healthDisplayBar.GetComponent<RectTransform>().sizeDelta.x;
        curMaxHealth = stats.healthMax;
    }

    // Health -------------------------------------------------------------------------------------------

    public virtual void UpdateHealthBar(float barValue)
    {
        healthDisplayBar.value = barValue;
        currentHealthLabel.text = stats.healthCurrent.ToString();
    }

    public virtual void UpdateMaxHealth()
    {
        ResizeHealthBar();
        UpdateMaxHealthLabel();
    }

    public virtual void ResizeHealthBar()
    {
        healthBarWidth = healthDisplayBar.GetComponent<RectTransform>().sizeDelta.x;
        float percentChange = (Mathf.Abs(stats.healthMax - curMaxHealth) / curMaxHealth) / 2.0f;
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

        curMaxHealth = stats.healthMax;
    }

    public virtual void UpdateMaxHealthLabel()
    {
        maxHealthLabel.text = "/ " + stats.healthMax.ToString();
    }
}

