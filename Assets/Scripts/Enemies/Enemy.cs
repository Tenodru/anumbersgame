using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy Spawn Object", order = 1)]
public class Enemy : ScriptableObject
{
    [Header("Enemy Attributes and References")]
    [Tooltip("This enemy's name.")] public string enemyName;
    [Tooltip("This enemy's prefab.")] public GameObject enemyChar;

    [Header("Enemy Spawn Attributes")]
    [Tooltip("The tier this enemy can begin to spawn in.")] public int enemySpawnTier;
    [Tooltip("The cost for the spawn manager to spawn this enemy.")] public int enemySpawnCost;
    [Tooltip("The experience granted when this enemy is destroyed.")] public int enemyXP;

    // Other attributes.
    float baseChance = 0f;
    float finalChance = 0f;

    public float GetBaseSpawnChance(int curSpawnTier)
    {
        float baseChance = 0f;

        // Return spawn chance of 0 if enemy is in higher tier.
        if (enemySpawnTier > curSpawnTier)
            return baseChance;

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

        return baseChance;
    }

    public float GetFinalSpawnChance(int[] counts)
    {
        float finalChance = 0f;

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
