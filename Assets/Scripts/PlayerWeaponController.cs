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
            for (int index = 0; index < typeSequence.Length; index++)
            {

            }

            //Instantiate(numberProjectiles[0], position: firePoint.position, Quaternion.identity);
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
