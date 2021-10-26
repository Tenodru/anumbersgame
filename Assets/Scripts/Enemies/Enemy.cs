using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Framework object for an Enemy.
/// Includes enemy name, prefab, spawn tier, spawn cost, and XP reward.
/// </summary>
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy Spawn Object", order = 1)]
public class Enemy : ScriptableObject
{
    [Header("Enemy Attributes and References")]
    [Tooltip("This enemy's name.")] public string enemyName;
    [Tooltip("This enemy's prefab.")] public GameObject enemyChar;

    [Header("Enemy Spawn Attributes")]
    [Tooltip("The tier this enemy can begin to spawn in.")] public int enemySpawnTier;
    [Tooltip("The cost for the spawn manager to spawn this enemy.")] public int enemySpawnCost;

    // Other attributes.
    float baseChance = 0f;
    float finalChance = 0f;

    /// <summary>
    /// Calculates base spawn chance and final sapwn chance.
    /// Base spawn chance is decided by current spawn tier and this enemy's spawn tier.
    /// Final spawn chance of this enemy is decided by dividing base spawn chance by all available enemies in this tier.
    /// </summary>
    /// <param name="curSpawnTier"></param>
    /// <param name="counts"></param>
    public void CalculateSpawnChances(int curSpawnTier, int[] counts)
    {
        CalcBaseSpawnChance(curSpawnTier);
        CalcFinalSpawnChance(counts);
    }

    public void CalcBaseSpawnChance(int curSpawnTier)
    {
        // Return spawn chance of 0 if enemy is in higher tier.
        if (enemySpawnTier > curSpawnTier)
            baseChance = 0f;

        if (curSpawnTier == 1)
        {
            baseChance = 1.0f;
        }
        else if (curSpawnTier == 2)
        {
            if (enemySpawnTier == 1)
                baseChance = 0.75f;
            if (enemySpawnTier == 2)
                baseChance = 0.25f;
        }
        else if (curSpawnTier == 3)
        {
            if (enemySpawnTier == 1)
                baseChance = 0.6f;
            if (enemySpawnTier == 2)
                baseChance = 0.3f;
            if (enemySpawnTier == 3)
                baseChance = 0.1f;
        }
        else if (curSpawnTier == 4)
        {
            if (enemySpawnTier == 1)
                baseChance = 0.5f;
            if (enemySpawnTier == 2)
                baseChance = 0.3f;
            if (enemySpawnTier == 3)
                baseChance = 0.125f;
            if (enemySpawnTier == 4)
                baseChance = 0.075f;
        }
    }

    public void CalcFinalSpawnChance(int[] counts)
    {
        finalChance = baseChance / counts[enemySpawnTier - 1];
    }

    /// <summary>
    /// Returns this enemy's base spawn chance.
    /// </summary>
    /// <returns>float | base spawn chance</returns>
    public float GetBaseSpawnChance()
    {
        return baseChance;
    }

    /// <summary>
    /// Returns this enemy's final spawn chance.
    /// </summary>
    /// <returns>float | final spawn chance</returns>
    public float GetFinalSpawnChance()
    {
        return finalChance;
    }

    public void SetBaseChance(float chance)
    {
        baseChance = chance;
    }

    public void SetFinalChance(float chance)
    {
        finalChance = chance;
    }

    public float GetBaseChance()
    {
        return baseChance;
    }

    public float GetFinalChance()
    {
        return finalChance;
    }
}
