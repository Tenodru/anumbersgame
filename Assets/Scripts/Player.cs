using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General handler class for Players. Source for Player component references.
/// </summary>
public class Player : MonoBehaviour
{
    public static Player current;
    public PlayerMovementController playerMoveControl;
    public PlayerWeaponController playerWeapControl;
    public PlayerStats playerStats;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMoveControl = GetComponent<PlayerMovementController>();
        playerWeapControl = GetComponent<PlayerWeaponController>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
