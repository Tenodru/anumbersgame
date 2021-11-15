using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("Pause Tracking")]
    public bool isPaused = false;
    public float timeLastPaused = 0.0f;
    public float totalTimePaused = 0.0f;

    [Header("References")]
    public Camera mainCam;

    public static PauseController current;

    // Other references.
    UpgradeHandler upgradeHandler;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        upgradeHandler = UpgradeHandler.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!upgradeHandler.upgradeScreenOpen)
            {
                mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Objects"));
                upgradeHandler.OpenUpgradeScreen();
                timeLastPaused = Time.deltaTime;
                Time.timeScale = 0;
            }
            else
            {
                mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Objects");
                upgradeHandler.CloseUpgradeScreen();
                totalTimePaused += Time.deltaTime - timeLastPaused;
                Time.timeScale = 1;
            }
        }
    }
}
