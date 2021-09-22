using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStandard : MonoBehaviour
{
    [Header("Basic Characteristics")]                                             //Basic projectile characteristics.
    [SerializeField] float baseSpeed = 1;

    // Other projectile characteristics.
    float currentSpeed;

    /// <summary>
    /// Gets this projectile's base speed.
    /// </summary>
    public float GetBaseSpeed()
    {
        return baseSpeed;
    }

    /// <summary>
    /// Gets this projectile's current speed.
    /// </summary>
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy this projectile when it goes out of bounds.
        if (collision.gameObject.tag == "Deathbox")
        {
            Destroy(this);
        }
    }
}
