using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Weapon Attributes")]
    [SerializeField] int startTypeLimit = 1;

    [Header("References")]
    [SerializeField] Transform firePoint;
    [SerializeField] TypeSystem typeSystem;
    [SerializeField] NumberHandler numberHandler;

    [Header("Events")]
    public UnityEvent firedWeapon;


    // Other weapon attributes.
    int curTypeLimit;
    List<GameObject> numberProjectiles;


    // Start is called before the first frame update
    void Start()
    {
        numberProjectiles = numberHandler.numberProjectiles;
        curTypeLimit = startTypeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        // Left mouse button pressed.
        if (Input.GetMouseButtonDown(0))
        {
            // Read type sequence.
            string typeSequence = typeSystem.GetTypeSequence();

            if (typeSequence == "")
            {
                return;
            }

            if (typeSequence.Length > 1)
            {
                GameObject compoundProj = Instantiate(numberHandler.compoundProjectile, position: firePoint.position, Quaternion.identity);
                compoundProj.GetComponent<ProjectileCompoundNumber>().CreateCompoundNumber(typeSequence);
                compoundProj.GetComponent<ProjectileCompoundNumber>().SetTeam(Teams.Player);
            }
            else {
                GameObject newProj = Instantiate(numberProjectiles[int.Parse(typeSequence)], position: firePoint.position, Quaternion.identity);
                newProj.GetComponent<ProjectileNumber>().SetTeam(Teams.Player);
            }

            Debug.Log("Fired weapon.");
            firedWeapon.Invoke();
        }
    }

    /// <summary>
    /// Returns the player's starting Typing Limit.
    /// </summary>
    /// <returns>int</returns>
    public int GetStartTypeLimit()
    {
        return startTypeLimit;
    }

    /// <summary>
    /// Returns the player's current Typing Limit.
    /// </summary>
    /// <returns>int</returns>
    public int GetCurTypeLimit()
    {
        return curTypeLimit;
    }
}
