using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStandard : MonoBehaviour
{
    [Header("Basic Characteristics")]                                             //Basic projectile characteristics.
    public bool disableMovement = false;
    public float baseSpeed = 1;

    // Other projectile characteristics.
    float currentSpeed;
    Vector3 moveDirection;

    public virtual void Start()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

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

    /// <summary>
    /// Gets this projectile's movement direction.
    /// </summary>
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(this.name + " collided with " + collider.gameObject.name);
        // Destroy this projectile when it goes out of bounds.
        if (collider.gameObject.tag == "Deathbox")
        {
            Debug.Log(this.name + " reached deathbox. Destroying now.");
            Destroy(this.gameObject);
        }
    }
}
