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
}
