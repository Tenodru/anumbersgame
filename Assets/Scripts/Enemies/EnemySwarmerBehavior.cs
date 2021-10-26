using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class that handles general behavior for the Swarmer enemy type. Inherits EnemyBehavior.
/// </summary>
public class EnemySwarmerBehavior : EnemyBehavior
{
    [Header("Movement Attributes")]
    [Tooltip("The starting orbit radius.")] public float radius = 10;
    [Tooltip("The starting orbit angle.")] public float angle = 0;

    float scaler = 1;
    float invokeTimeRadius = 0.1f;
    float invokeTimeScaler = 0.1f;

    float alpha = 1.0f;

    SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        StartCoroutine(DecreaseRadius(invokeTimeRadius));
        StartCoroutine(IncreaseScaler(invokeTimeScaler));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Elliptical path
        transform.position = new Vector2(0f + (10f * Mathf.Sin(Mathf.Deg2Rad * alpha)), 0f + (5f * Mathf.Cos(Mathf.Deg2Rad * alpha)));
        alpha += 0.01f;
        */
        
        float x = 0;
        float y = 0;

        Vector2 direction = Vector2.zero;

        x = radius * Mathf.Cos(angle);
        y = radius * Mathf.Sin(angle);

        transform.position = new Vector2(x, y);

        angle += 15 * Mathf.Deg2Rad * Time.deltaTime * scaler;
    }

    IEnumerator DecreaseRadius(float time)
    {
        yield return new WaitForSeconds(invokeTimeRadius);
        while (true)
        {
            if (radius > 0)
                radius -= 0.01f;
            //Debug.Log("Radius: " + radius);
            if (invokeTimeRadius > 0.0002)
            {
                invokeTimeRadius -= 0.0002f;
            }
            yield return new WaitForSeconds(invokeTimeRadius);
        }
    }

    IEnumerator IncreaseScaler(float time)
    {
        yield return new WaitForSeconds(invokeTimeScaler);
        while (true)
        {
            //Debug.Log("Scaler: " + scaler);
            if (scaler < 16)
            {
                scaler += 0.01f;
            }
            if (invokeTimeScaler > 0.0002)
            {
                invokeTimeScaler -= 0.0002f;
            }
            
            yield return new WaitForSeconds(invokeTimeScaler);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // Decrease player health by this enemy's damage stat.
            //Debug.Log("Player took damage from " + this.gameObject.name);
            enemyKilled.Invoke();
            spawnManager.UpdateEnemyCount(-1);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag == "Projectile")
        {
            //Debug.Log(this.gameObject.name + " hit by projectile!");
            // Check to see if projectile is from player.
            if (collider.GetComponent<ProjectileStandard>().team == Teams.Player)
            {
                // Check if projectile has a type that matches one of this enemy's weaknesses.
                for (int i = 0; i < collider.GetComponent<ProjectileStandard>().projectileTypes.Count; i++)
                {
                    if (projectileWeaknesses.Contains(collider.GetComponent<ProjectileStandard>().projectileTypes[i]))
                    {
                        //Debug.Log(this.gameObject.name + " successfully destroyed by player projectile!");
                        enemyKilled.Invoke();
                        spawnManager.UpdateEnemyCount(-1);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    /*
    void ChangeRadius()
    {
        if (invokeTime > 0.0001)
        {
            invokeTime -= 0.0001f;
            Invoke("DecreaseRadius", invokeTime);
        }
    }

    void DecreaseRadius()
    {
        if (radius > 0)
        {
            radius -= 0.01f;
        }
    }

    void IncreaseScaler()
    {
        if (scaler < 16)
        {
            scaler += 0.01f;
        }
    }
    */
}
