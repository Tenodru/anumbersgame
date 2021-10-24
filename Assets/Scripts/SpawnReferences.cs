using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReferences : MonoBehaviour
{
    [Header("Enemies")]                                             //References to enemy assets to be spawned and their spawn costs.
    public Enemy enemy1;
    public Enemy enemy2;
    public Enemy enemy3;
    public Enemy enemy4;

    public List<Enemy> enemies;

    public int[] GetTierCount ()
    {
        int[] counts = new int[4];

        foreach (Enemy enemy in enemies)
        {
            counts[enemy.enemySpawnTier - 1] += 1;
        }

        return counts;
    }
}
