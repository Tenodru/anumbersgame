using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles global stuff like tracking time.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [Tooltip("Time elapsed since game start.")]
    public float elapsedTime = 0;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartTime()
    {
        elapsedTime = Time.time;
    }
}
