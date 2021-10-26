using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles number prefab logging and references.
/// </summary>
public class NumberHandler : MonoBehaviour
{
    public List<GameObject> numberProjectiles;
    public List<GameObject> compoundProjectiles;

    public GameObject compoundProjectile;

    public float projectileWidth = 0.55f;
    public float projectileHeight = 0.83f;

    public void Start()
    {
        //projectileWidth = numberProjectiles[0].GetComponent<BoxCollider2D>().bounds.size.x;
    }

}
