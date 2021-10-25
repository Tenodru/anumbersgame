using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reference collection for spawns.
/// </summary>
public class SpawnReferences : MonoBehaviour
{
    [Header("Enemies")]                                             //References to enemy assets to be spawned and their spawn costs.
    public Enemy enemy1;
    public Enemy enemy2;
    public Enemy enemy3;
    public Enemy enemy4;

    [Tooltip("List of all enemies as SpawnEntries.")] public List<SpawnEntry> enemies;
    [Tooltip("All SpawnTiers. Contains SpawnCategories, groups of enemies with associated spawn chances.")] public List<SpawnTier> spawnTiers;

    /// <summary>
    /// Returns an array of enemy counts for each spawn tier at the current moment.
    /// Each index of returned array corresponds to a spawn tier (tier = index + 1)
    /// </summary>
    /// <returns>array<int> | array of enemy counts</int></returns>
    public int[] GetTierCount ()
    {
        int[] counts = new int[4];

        foreach (SpawnTier tier in spawnTiers)
        {
            foreach (SpawnCategory cat in tier.categories)
            {
                foreach (SpawnEntry entry in cat.enemies)
                {
                    if (entry.available)
                        counts[entry.enemy.enemySpawnTier - 1] += 1;
                }
            }
        }

        return counts;
    }
}

/// <summary>
/// A spawn entry for an enemy. Contains an Enemy and a boolean value determining Enemy availability
/// at the current moment. Availability determines if Enemy can spawn. If false, Enemy will not spawn
/// even if it falls within the current spawn tier. Use for progression ("unlock" enemies).
/// </summary>
[System.Serializable]
public class SpawnEntry
{
    [Tooltip("Enemy variable.")] public Enemy enemy;
    [Tooltip("Whether enemy is available to spawn currently.")] public bool available = true;
}

/// <summary>
/// A spawn category. Defines a group of enemies and their spawn chance.
/// </summary>
[System.Serializable]
public class SpawnCategory
{
    [Tooltip("Enemy spawn entries.")] public List<SpawnEntry> enemies;
    [Tooltip("The overall spawn chance for this category.")] [Range(0, 1)] public float spawnChance;
}

/// <summary>
/// A spawn tier. Contains spawn Categories.
/// </summary>
[System.Serializable]
public class SpawnTier
{
    [Tooltip("The numeric value for this tier.")] public int tierID;
    [Tooltip("Enemy spawn categories.")] public List<SpawnCategory> categories;
}
