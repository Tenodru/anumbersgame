using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the creation of compound number projectiles.
/// </summary>
public class CompoundNumberHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] NumberHandler numberHandler;

    /// <summary>
    /// Creates a projectile with multiple numbers, following a given typeSequence string.
    /// </summary>
    /// <param name="typeSequence"></param>
    public void CreateCompoundNumber (string typeSequence)
    {
        for (int index = 0; index < typeSequence.Length; index++)
        {

        }
    }
}
