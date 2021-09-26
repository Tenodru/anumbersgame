using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles enemy spawning.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [Header("Spawnpoint References")]                               //Spawnpoint area center reference points.
    [SerializeField] Transform upperSpawnPoint;
    [SerializeField] Transform lowerSpawnPoint;
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform rightSpawnPoint;

    [Header("Spawn Area Characteristics")]                          //The attributes of the spawn areas.
    [Tooltip("The length of the upper and lower spawn areas.")]
    [SerializeField] float spawnAreaLengthUL;
    [Tooltip("The height of the upper and lower spawn areas.")]
    [SerializeField] float spawnAreaHeightUL;
    [Tooltip("The length of the left and right spawn areas.")]
    [SerializeField] float spawnAreaLengthLR;
    [Tooltip("The height of the left and right spawn areas.")]
    [SerializeField] float spawnAreaHeightLR;

    [Header("Other Attributes")]                                    //Other changeable attributes that affect wave spawning.
    [Tooltip("The difficulty of the game, used in calculating the amount of enemies spawned with each 'wave'.")]
    [Range(1, 4)] [SerializeField] int difficulty = 1;
    [Tooltip("How high the spawn budget can go before being capped.")]
    [SerializeField] int spawnBudgetCap = 20;

    [Header("Wave Calculator")]                                     //A calculator that displays how many enemies will be spawned in each wave.
    [Space(20)]
    [Range(1, 4)] [SerializeField] int exampleDifficulty = 1;
    [SerializeField] float exampleTime = 1;
    [SerializeField] int exampleWave = 1;
    int exampleTimeSpawnScale;
    int exampleSpawnTier;

    SpawnReferences spawnReferences;

    float nextSpawnTime = 0.0f;
    int enemiesKilled = 0;
    int currentEnemyCount = 0;
    int wave = 1;
    int spawnTier = 1;
    int timeSpawnScale;
    int currentTime;

    int spawnBudget = 0;
    int spawnBudgetThisWave = 0;

    private void Start()
    {
        spawnReferences = FindObjectOfType<SpawnReferences>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = (int)Time.time;

        if ((int)(Time.time / 30) <= 0)
            spawnTier = 1;
        else if ((int)(Time.time / 30) >= 5)
            spawnTier = 5;
        else spawnTier = (int)(Time.time / 30) + 1;

        if (spawnTier == 2)
        {
            //enemy1SpawnChance = 0.3f;
            //enemy2SpawnChance = 0.65f;
        }
        if (spawnTier >= 3)
        {
            //enemy1SpawnChance = 0.4f;
            //enemy2SpawnChance = 0.55f;
        }

        if (currentEnemyCount <= difficulty * 2)
        {
            timeSpawnScale = (int)(Time.time / 10);

            if ((int)(((int)(Mathf.Sqrt(wave)) / 2) * difficulty * timeSpawnScale) < 5)
                spawnBudget = 5;
            else if ((int)(((int)(Mathf.Sqrt(wave)) / 2) * difficulty * timeSpawnScale) >= 20)
                spawnBudget = 20;
            else spawnBudget = (int)(((int)(Mathf.Sqrt(wave)) / 2) * difficulty * timeSpawnScale);

            spawnBudgetThisWave = spawnBudget;
            wave += 1;
            SpawnEnemies();
        }
    }

    /// <summary>
    /// Spawns enemies when called.
    /// </summary>
    public void SpawnEnemies()
    {
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        int spawnAmount = (int)((Mathf.Sqrt(wave) / 2) * difficulty * 3);
        
        while (spawnBudget > 0)
        {
            float spawnAreaChance = Random.Range(0, 1.0f);

            if (spawnAreaChance <= 0.25f)
            {
                Vector3 randPoint = new Vector3(Random.Range(upperSpawnPoint.position.x - spawnAreaLengthUL, upperSpawnPoint.position.x + spawnAreaLengthUL), Random.Range(upperSpawnPoint.position.y - spawnAreaHeightUL, upperSpawnPoint.position.y + spawnAreaHeightUL), upperSpawnPoint.position.z);
                
                // Spawn enemy1.
                GameObject newEnemy1 = Instantiate(spawnReferences.enemy1.enemyChar, randPoint, rot);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().angle = Random.Range(0, 50);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().radius = Random.Range(8, 12);

                currentEnemyCount += 1;
                spawnBudget -= spawnReferences.enemy1.enemySpawnCost;
            }
            if (spawnAreaChance > 0.25f && spawnAreaChance <= 0.5f)
            {
                Vector3 randPoint = new Vector3(Random.Range(lowerSpawnPoint.position.x - spawnAreaLengthUL, lowerSpawnPoint.position.x + spawnAreaLengthUL), Random.Range(lowerSpawnPoint.position.y - spawnAreaHeightUL, lowerSpawnPoint.position.y + spawnAreaHeightUL), lowerSpawnPoint.position.z);

                // Spawn enemy1.
                GameObject newEnemy1 = Instantiate(spawnReferences.enemy1.enemyChar, randPoint, rot);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().angle = Random.Range(0, 50);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().radius = Random.Range(8, 12);

                currentEnemyCount += 1;
                spawnBudget -= spawnReferences.enemy1.enemySpawnCost;
            }
            if (spawnAreaChance > 0.50f && spawnAreaChance <= 0.75f)
            {
                Vector3 randPoint = new Vector3(Random.Range(leftSpawnPoint.position.x - spawnAreaLengthLR, leftSpawnPoint.position.x + spawnAreaLengthLR), Random.Range(leftSpawnPoint.position.y - spawnAreaHeightLR, leftSpawnPoint.position.y + spawnAreaHeightLR), lowerSpawnPoint.position.z);

                // Spawn enemy1.
                GameObject newEnemy1 = Instantiate(spawnReferences.enemy1.enemyChar, randPoint, rot);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().angle = Random.Range(0, 50);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().radius = Random.Range(8, 12);

                currentEnemyCount += 1;
                spawnBudget -= spawnReferences.enemy1.enemySpawnCost;
            }
            if (spawnAreaChance > 0.75f)
            {
                Vector3 randPoint = new Vector3(Random.Range(rightSpawnPoint.position.x - spawnAreaLengthLR, rightSpawnPoint.position.x + spawnAreaLengthLR), Random.Range(rightSpawnPoint.position.y - spawnAreaHeightLR, rightSpawnPoint.position.y + spawnAreaHeightLR), lowerSpawnPoint.position.z);

                // Spawn enemy1.
                GameObject newEnemy1 = Instantiate(spawnReferences.enemy1.enemyChar, randPoint, rot);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().angle = Random.Range(0, 50);
                newEnemy1.GetComponent<EnemySwarmerBehavior>().radius = Random.Range(8, 12);

                currentEnemyCount += 1;
                spawnBudget -= spawnReferences.enemy1.enemySpawnCost;
            }
        }
    }

    /// <summary>
    /// Updates the current enemy count.
    /// </summary>
    /// <param name="change"></param>
    public void UpdateEnemyCount(int change)
    {
        currentEnemyCount += change;
        Debug.Log("Updated enemy count.");
    }

    /// <summary>
    /// Updates the player's kill count.
    /// </summary>
    public void UpdateEnemiesKilled()
    {
        enemiesKilled += 1;
        currentEnemyCount -= 1;
    }

    public int UICalculateSpawnBudget()
    {
        int exampleSpawnBudget = 0;

        exampleTimeSpawnScale = (int)(exampleTime / 10);

        if ((int)(exampleTime / 30) <= 0)
            exampleSpawnTier = 1;
        else if ((int)(exampleTime / 30) >= 5)
            exampleSpawnTier = 5;
        else exampleSpawnTier = (int)(exampleTime / 30);

        if ((int)(((int)(Mathf.Sqrt(exampleWave)) / 2) * exampleDifficulty * exampleTimeSpawnScale) < 5)
            exampleSpawnBudget = 5;
        else if ((int)(((int)(Mathf.Sqrt(exampleWave)) / 2) * exampleDifficulty * exampleTimeSpawnScale) >= 20)
            exampleSpawnBudget = 20;
        else exampleSpawnBudget = (int)(((int)(Mathf.Sqrt(exampleWave)) / 2) * exampleDifficulty * exampleTimeSpawnScale);

        return exampleSpawnBudget;
    }

    public int UITimeScale()
    {
        return exampleTimeSpawnScale;
    }

    public int UISpawnTier()
    {
        return exampleSpawnTier;
    }

    /// <summary>
    /// Draws boxes representing the enemy spawn areas in the editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (upperSpawnPoint == null || lowerSpawnPoint == null || leftSpawnPoint == null || rightSpawnPoint == null)
            return;

        Vector3 areaSize = new Vector3(spawnAreaLengthUL, spawnAreaHeightUL);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(upperSpawnPoint.position, areaSize);
        Gizmos.DrawWireCube(lowerSpawnPoint.position, areaSize);

        areaSize = new Vector3(spawnAreaLengthLR, spawnAreaHeightLR);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(leftSpawnPoint.position, areaSize);
        Gizmos.DrawWireCube(rightSpawnPoint.position, areaSize);
    }
}
