using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNumber : ProjectileStandard
{
    [Header("Number Projectile Characteristics")]                                             //Number projectile characteristics.
    [SerializeField] Teams team;

    // Other projectile characteristics.

    /// <summary>
    ///  Returns this projectile's Team.
    /// </summary>
    public virtual Teams GetTeam()
    {
        return team;
    }

    // Extends base projectile collision check.
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // This is a Player-fired projectile.
        if (team == Teams.Player)
        {
            // Projectile collides with another Attack projectile.
            if (collision.gameObject.tag == "ProjectileAttack")
            {
                // Collision object is an Enemy-fired projectile.
                if (collision.gameObject.GetComponent<ProjectileNumber>().GetTeam() == Teams.Enemy)
                {
                    // Check that numbers match, etc.
                }
            }
        }
    }
}
