using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the creation of compound number projectiles.
/// </summary>
public class ProjectileCompoundNumber : ProjectileNumber
{
    [Header("References")]
    [SerializeField] NumberHandler numberHandler;

    private void Awake()
    {
        if (numberHandler == null)
        {
            numberHandler = GameObject.FindObjectOfType<NumberHandler>();
        }
    }

    /// <summary>
    /// Creates a projectile with multiple numbers, following a given typeSequence string.
    /// </summary>
    /// <param name="typeSequence"></param>
    public void CreateCompoundNumber (string typeSequence)
    {
        float totalLength = typeSequence.Length * numberHandler.projectileWidth;
        float spacerW = totalLength / (typeSequence.Length * 2);
        float spacerH = numberHandler.projectileHeight;

        int nextSpot = 1;
        int spotIndexL = Mathf.RoundToInt(typeSequence.Length / 2);
        int spotIndexR = Mathf.RoundToInt(typeSequence.Length / 2);

        int columns = 3;
        float rowCount = Mathf.Ceil(typeSequence.Length / (float)columns);

        if (typeSequence.Length == 2)
        {
            GameObject newProj = Instantiate(numberHandler.compoundProjectiles[(int)Char.GetNumericValue(typeSequence[0])], this.gameObject.transform);
            newProj.transform.localPosition = new Vector3(-spacerW, 0, 0);

            newProj = Instantiate(numberHandler.compoundProjectiles[(int)Char.GetNumericValue(typeSequence[1])], this.gameObject.transform);
            newProj.transform.localPosition = new Vector3(spacerW, 0, 0);
        }
        else if (typeSequence.Length == 3)
        {
            GameObject newProj = Instantiate(numberHandler.compoundProjectiles[(int)Char.GetNumericValue(typeSequence[0])], this.gameObject.transform);
            newProj.transform.localPosition = new Vector3(-spacerW * 2, 0, 0);

            newProj = Instantiate(numberHandler.compoundProjectiles[(int)Char.GetNumericValue(typeSequence[1])], this.gameObject.transform);
            newProj.transform.localPosition = new Vector3(0, 0, 0);

            newProj = Instantiate(numberHandler.compoundProjectiles[(int)Char.GetNumericValue(typeSequence[2])], this.gameObject.transform);
            newProj.transform.localPosition = new Vector3(spacerW * 2, 0, 0);
        }

        /*
        for (int index = 0; index < typeSequence.Length; index++)
        {
            Debug.Log("Next num: " + typeSequence[index]);
            Debug.Log("Char type: " + (typeSequence[index]).GetType());

            // Instantiate number projectile and attach to this compoundNumber parent.
            GameObject newProj = Instantiate(numberHandler.compoundProjectiles[(int)Char.GetNumericValue(typeSequence[index])], this.gameObject.transform);
            newProj.transform.localPosition = new Vector3(0, 0, 0);

            int row = index / columns;
            int column = index % columns;
            float projPos = nextSpot * spacerW;

            


            // Even row count.
            /*
            if (rowCount % 2 == 0)
            {
                newProj.transform.localPosition = new Vector3(column * spacerW - spacerW, row * spacerH - spacerH / 2, 0);
            }
            else
            {
                newProj.transform.localPosition = new Vector3(column * spacerW - spacerW, row * spacerH - spacerH / 2, 0);
            }

            // Reposition number.
            // Even sequence length.
            if (typeSequence.Length % 2 == 0)
            {
                if (nextSpot <= typeSequence.Length / 2)
                {
                    // Relative position this number should be at.
                    float projPos = spotIndexL * spacer - spacer;

                    newProj.transform.localPosition = new Vector3(-column * spacer, row * spacer, 0);
                    Debug.Log("Shifted num left.");
                    nextSpot++;
                    spotIndexL--;
                }
                else
                {
                    // Relative position this number should be at.
                    float projPos = spotIndexR * spacer + spacer;

                    newProj.transform.localPosition = new Vector3(column * spacer, row * spacer, 0);
                    Debug.Log("Shifted num right.");
                    nextSpot++;
                    spotIndexR--;
                }
            }

            // Odd sequence length.
            else
            {
                if (nextSpot <= typeSequence.Length / 2)
                {
                    // Relative position this number should be at.
                    float projPos = spotIndexL * spacer;

                    newProj.transform.localPosition = new Vector3(-projPos, 0, 0);
                    Debug.Log("Shifted num left.");
                    nextSpot++;
                    spotIndexL++;
                }
                else
                {
                    // Relative position this number should be at.
                    float projPos = spotIndexR * spacer;

                    newProj.transform.localPosition = new Vector3(projPos, 0, 0);
                    Debug.Log("Shifted num right.");
                    nextSpot++;
                    spotIndexR++;
                }
            }
        }*/
    }
}
