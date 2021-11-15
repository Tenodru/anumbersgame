using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] PlayerWeaponController player;

    // Other variables.
    int typeCount = 0;
    string typeSequence = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseController.current.isPaused)
            return;
        CheckTyping();
    }

    /// <summary>
    /// Tracks the player's typing.
    /// </summary>
    public void CheckTyping()
    {
        if (typeCount < player.GetCurTypeLimit())
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                typeSequence += "0";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                typeSequence += "1";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                typeSequence += "2";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                typeSequence += "3";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                typeSequence += "4";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                typeSequence += "5";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                typeSequence += "6";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                typeSequence += "7";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                typeSequence += "8";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                typeSequence += "9";
                textDisplay.text = "[ " + typeSequence + " ]";
                typeCount++;
                Debug.Log("Sequence: " + typeSequence);
            }
        }
    }

    /// <summary>
    /// Clears the current type sequence.
    /// </summary>
    public void ClearTypeSequence()
    {
        typeCount = 0;
        typeSequence = "";
        textDisplay.text = "[ " + typeSequence + " ]";
    }

    /// <summary>
    /// Returns the current type sequence.
    /// </summary>
    /// <returns>string</returns>
    public string GetTypeSequence()
    {
        return typeSequence;
    }
}
