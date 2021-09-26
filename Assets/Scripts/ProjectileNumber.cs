using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNumber : ProjectileStandard
{

    // Other projectile characteristics.


    public virtual void Update()
    {
        transform.position = transform.position + GetMoveDirection() * baseSpeed * Time.deltaTime;
    }

    // Extends base projectile collision check.
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        // This is a Player-fired projectile.
        if (team == Teams.Player)
        {
            // Projectile collides with another Attack projectile.
            if (collider.gameObject.tag == "ProjectileAttack")
            {
                // Collision object is an Enemy-fired projectile.
                if (collider.gameObject.GetComponent<ProjectileNumber>().team == Teams.Enemy)
                {
                    // Check that numbers match, etc.
                }
            }
        }
    }
}
