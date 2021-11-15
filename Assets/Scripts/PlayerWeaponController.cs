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
    PlayerStats stats;
    StatsDisplay statDisplay;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.current;
        statDisplay = GetComponent<StatsDisplay>();
        numberProjectiles = numberHandler.numberProjectiles;
        curTypeLimit = startTypeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        UpdateFuel();
    }

    private void FixedUpdate()
    {
        
    }

    public void Fire()
    {
        // Left mouse button pressed.
        if (Input.GetMouseButtonDown(0))
        {
            // Read type sequence.
            string typeSequence = typeSystem.GetTypeSequence();
            float curFuel = stats.fuel;

            // Don't fire if nothing is loaded, or if the fuel consumption would be higher than the fuel we currently have.
            if (typeSequence == "" || (curFuel -= stats.baseFuelConsumption * typeSequence.Length) < 0)
            {
                return;
            }

            if (typeSequence.Length > 1)
            {
                GameObject compoundProj = Instantiate(numberHandler.compoundProjectile, position: firePoint.position, Quaternion.identity);
                compoundProj.GetComponent<ProjectileCompoundNumber>().CreateCompoundNumber(typeSequence);
                compoundProj.GetComponent<ProjectileCompoundNumber>().team = Teams.Player;
                compoundProj.GetComponent<ProjectileCompoundNumber>().originPlayer = GetComponent<Player>();
                compoundProj.GetComponent<ProjectileCompoundNumber>().baseSpeed = 4;
            }
            else {
                GameObject newProj = Instantiate(numberProjectiles[int.Parse(typeSequence)], position: firePoint.position, Quaternion.identity);
                newProj.GetComponent<ProjectileNumber>().team = Teams.Player;
                newProj.GetComponent<ProjectileNumber>().originPlayer = GetComponent<Player>();
                newProj.GetComponent<ProjectileNumber>().baseSpeed = 4;
            }

            // Consume fuel based on length of typeSequence.
            float change = stats.baseFuelConsumption * typeSequence.Length;
            stats.fuel -= change;
            statDisplay.UpdateFuelBar(stats.fuel / stats.maxFuel * 100);
            Debug.Log("Fired fuel: " + (stats.fuel / stats.maxFuel * 100));

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

    public void UpdateFuel()
    {
        timer += Time.deltaTime;

        if (timer >= stats.fuelRefillDelay)
        {
            timer = 0f;
            float change = (1 * stats.fuelRefillMultiplier);
            if (stats.fuel + change > stats.maxFuel)
            {
                //statDisplay.ChangeFuelDisplay(stats.maxFuel - stats.fuel, stats.maxFuel);
                statDisplay.UpdateFuelBar(100);
                stats.fuel = stats.maxFuel;
            }
            else
            {
                stats.fuel += change;
                //statDisplay.ChangeFuelDisplay(change, stats.fuel);
                statDisplay.UpdateFuelBar(stats.fuel / stats.maxFuel * 100);
            }
        }
    }
}
