using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for enemy behavior.
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public float charVelocity = 2f;
    [Tooltip("The damage this enemy will do to player health.")] public float damage = 1.0f;

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
