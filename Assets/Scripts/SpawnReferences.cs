using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    [Header("Tier Spawn Chance Calculator")]                        //A calculator that displays the spawn chance for each tier based on current tier.
    [Space(20)]
    [Range(1, 4)] [SerializeField] int exampleTierCur = 1;
    [SerializeField] List<SpawnTier> exampleTiers;

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

    /// <summary>
    /// Calculates and sets spawn chances for all tiers in spawnTiers.
    /// </summary>
    /// <param name="currentTier"></param>
    public void CalculateTierSpawnChances(int currentTier)
    {
        foreach (SpawnTier tier in spawnTiers)
        {
            // Return spawn chance of 0 if tier is higher than current tier.
            if (tier.tierID > currentTier)
                tier.SetSpawnChance(0);

            if (currentTier == 1)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(1);
            }
            else if (currentTier == 2)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(0.75f);
                if (tier.tierID == 2)
                    tier.SetSpawnChance(0.25f);
            }
            else if (currentTier == 3)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(0.6f);
                if (tier.tierID == 2)
                    tier.SetSpawnChance(0.3f);
                if (tier.tierID == 3)
                    tier.SetSpawnChance(0.1f);
            }
            else if (currentTier == 4)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(0.5f);
                if (tier.tierID == 2)
                    tier.SetSpawnChance(0.3f);
                if (tier.tierID == 3)
                    tier.SetSpawnChance(0.125f);
                if (tier.tierID == 4)
                    tier.SetSpawnChance(0.075f);
            }
        }

        // Sort spawnTier list from smallest spawnChance to largest spawnChance.
        spawnTiers = spawnTiers.OrderBy(x => x.GetSpawnChance()).ToList();
        foreach (SpawnTier tier in spawnTiers)
        {
            Debug.Log("Tier: " + tier.tierID + " ... with spawnChance: " + tier.GetSpawnChance());
        }
        Debug.Log("Count: " + spawnTiers.Count);
    }

    /// <summary>
    /// Sorts the SpawnCategories of the given SpawnTier in ascending order of category spawnChance.
    /// </summary>
    /// <param name="tier"></param>
    public void SortCatSpawnChances(SpawnTier tier)
    {
        tier.categories = tier.categories.OrderBy(x => x.spawnChance).ToList();
    }

    /// <summary>
    /// Selects a SpawnTier based on given chance.
    /// </summary>
    /// <param name="chance"></param>
    /// <returns></returns>
    public SpawnTier SelectTier(float chance)
    {
        // Parse through spawnTiers list, from smallest spawnChance to largest spawnChance, and compare chance with tier.spawnChance.
        // Select the first tier for which chance falls under tier.spawnChance.
        foreach (SpawnTier tier in spawnTiers)
        {
            if (chance < tier.GetSpawnChance())
            {
                SortCatSpawnChances(tier);
                return tier;
            }
        }

        return null;
    }

    /// <summary>
    /// Selects a SpawnCategory based on given SpawnTier and chance.
    /// </summary>
    /// <param name="tier"></param>
    /// <param name="chance"></param>
    /// <returns></returns>
    public SpawnCategory SelectCategory(SpawnTier tier, float chance)
    {
        // Parse through spawnCategories list, from smallest spawnChance to largest spawnChance, and compare chance with cat.spawnChance.
        // Select the first category for which chance falls under cat.spawnChance.
        foreach (SpawnCategory cat in tier.categories)
        {
            if (chance < cat.spawnChance)
            {
                return cat;
            }
        }

        return null;
    }


    // ------------------------------------------------------------------------------------------------------

    public void UICalculateSpawnChances()
    {
        foreach (SpawnTier tier in exampleTiers)
        {
            // Return spawn chance of 0 if tier is higher than current tier.
            if (tier.tierID > exampleTierCur)
                tier.SetSpawnChance(0);

            if (exampleTierCur == 1)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(1);
            }
            else if (exampleTierCur == 2)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(0.75f);
                if (tier.tierID == 2)
                    tier.SetSpawnChance(0.25f);
            }
            else if (exampleTierCur == 3)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(0.6f);
                if (tier.tierID == 2)
                    tier.SetSpawnChance(0.3f);
                if (tier.tierID == 3)
                    tier.SetSpawnChance(0.1f);
            }
            else if (exampleTierCur == 4)
            {
                if (tier.tierID == 1)
                    tier.SetSpawnChance(0.5f);
                if (tier.tierID == 2)
                    tier.SetSpawnChance(0.3f);
                if (tier.tierID == 3)
                    tier.SetSpawnChance(0.125f);
                if (tier.tierID == 4)
                    tier.SetSpawnChance(0.075f);
            }
        }
        // Sort spawnTier list from smallest spawnChance to largest spawnChance.
        spawnTiers = spawnTiers.OrderBy(x => x.GetSpawnChance()).ToList();
    }
    public float UIGetTier1Chance()
    {
        return exampleTiers[0].GetSpawnChance();
    }
    public float UIGetTier2Chance()
    {
        return exampleTiers[1].GetSpawnChance();
    }
    public float UIGetTier3Chance()
    {
        return exampleTiers[2].GetSpawnChance();
    }
    public float UIGetTier4Chance()
    {
        return exampleTiers[3].GetSpawnChance();
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

    float spawnChance;

    public void SetSpawnChance(float chance)
    {
        spawnChance = chance;
    }
    public float GetSpawnChance()
    {
        return spawnChance;
    }
}
