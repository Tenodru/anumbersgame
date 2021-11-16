using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class that handles general enemy behavior. Specific enemy types will use their own behavior class that inherits this one.
/// See EnemySwarmerBehavior as an inheritance example.
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public float charVelocity = 2f;
    [Tooltip("The damage this enemy will do to player health.")]
    public float damage = 1.0f;
    [Tooltip("List of projectiles that can damage this enemy.")]
    public List<ProjectileType> projectileWeaknesses;
    [Tooltip("The experience granted when this enemy is destroyed.")]
    public int enemyXP;
    [Tooltip("The current health of this enemy.")]
    public float healthCurrent;
    [Tooltip("The max health of this enemy.")]
    public float healthMax;

    [Header("Events")]
    public UnityEvent enemyKilled;

    // Other references.
    GameObject player;
    SpawnManager spawnManager;
    StatsDisplayEnemy statDisplay;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        spawnManager = SpawnManager.current;
        statDisplay = GetComponent<StatsDisplayEnemy>();
        healthCurrent = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // Decrease player health by this enemy's damage stat.
            collider.GetComponent<Player>().playerStats.TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage from " + gameObject.name);
            enemyKilled.Invoke();
            spawnManager.UpdateEnemyCount(-1);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag == "Projectile")
        {
            // Check to see if projectile is from player.
            if (collider.GetComponent<ProjectileStandard>().team == Teams.Player)
            {
                // Check if projectile has a type that matches one of this enemy's weaknesses.
                for (int i = 0; i < collider.GetComponent<ProjectileStandard>().projectileTypes.Count; i++)
                {
                    if (projectileWeaknesses.Contains(collider.GetComponent<ProjectileStandard>().projectileTypes[i]))
                    {
                        //Debug.Log(this.gameObject.name + " successfully destroyed by player projectile!");
                        
                        Destroy(collider.gameObject);

                        // Decrease health by projectile damage.
                        TakeDamage(collider.GetComponent<ProjectileStandard>().damage, collider.gameObject);
                    }
                }
            }
        }
    }

    public virtual void TakeDamage(float damage, GameObject source)
    {
        if (healthCurrent - damage <= 0)
        {
            statDisplay.UpdateHealthBar(0);
            healthCurrent = 0;
            source.GetComponent<ProjectileNumber>().originPlayer.playerStats.GainXP(enemyXP);             // Reward player with XP.
            spawnManager.UpdateEnemyCount(-1);
            enemyKilled.Invoke();
            Destroy(gameObject);
            return;
        }
        healthCurrent -= damage;
        statDisplay.UpdateHealthBar((healthCurrent / healthMax) * 100);
        Debug.Log("Enemy Health: " + healthCurrent);
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
