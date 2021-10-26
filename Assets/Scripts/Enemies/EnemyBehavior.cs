using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class that handles general enemy behavior. Specific enemy types will use their own behavior class that inherits this one.
/// See EnemySwarmerbehavior as an inheritance example.
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public float charVelocity = 2f;
    [Tooltip("The damage this enemy will do to player health.")] public float damage = 1.0f;
    [Tooltip("List of projectiles that can damage this enemy.")] public List<ProjectileType> projectileWeaknesses;

    [Header("Events")]
    public UnityEvent enemyKilled;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Reference the Player gameObject.
    /// </summary>
    /// <returns></returns>
    public virtual GameObject GetPlayer()
    {
        return player;
    }
}
