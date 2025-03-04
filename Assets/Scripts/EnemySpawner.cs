using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;  // Ensure each spawnPoint has a 2D trigger collider
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float bpm = 120;
    [SerializeField] private float spawnOffset = 0.5f;
    [SerializeField] private float chanceToNotSpawn = 0.25f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject _enemies;
    private bool isPlaying = false;
    private bool skipSpawn = false;

    [Header("Spawn Probability Settings")]
    [SerializeField, Range(0f, 1f)] private float chanceEnemy2 = 0.2f; // 20% chance for enemy[2]
    [SerializeField, Range(0f, 1f)] private float chanceEnemy3 = 0.1f; // 10% chance for enemy[3]

    [Header("Debug")]
    [SerializeField] private bool showSpawnGizmos = true;

    private float spawnInterval;
    private float timeSinceLastSpawn;
    private int spawnIndex = 0; // Alternate between left and right

    // Track enemies in each spawn area
    private List<Collider2D>[] enemiesInSpawnArea;

    void Awake()
    {
        CalculateSpawnInterval();
        ValidateSpawnPoints();
        InitializeSpawnAreaTracking();
        timeSinceLastSpawn = 0f;
    }

    private void Start()
    {
        bpm = BPMReference.Bpm.bpm;
    }

    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.fixedDeltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
            timeSinceLastSpawn = 0f;
        }
    }

    void CalculateSpawnInterval()
    {
        spawnInterval = 60f / bpm;
    }

    void ValidateSpawnPoints()
    {
        if (spawnPoints.Length != 2)
        {
            Debug.LogError("Нужно 2 точки спавна!");
            enabled = false;
        }
    }

    void InitializeSpawnAreaTracking()
    {
        enemiesInSpawnArea = new List<Collider2D>[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemiesInSpawnArea[i] = new List<Collider2D>();

            // Add Trigger Handler for each spawn point
            SpawnTrigger trigger = spawnPoints[i].gameObject.AddComponent<SpawnTrigger>();
            trigger.Initialize(this, i);
        }
    }

    public void RegisterEnemyInSpawnArea(int spawnPointIndex, Collider2D enemy)
    {
        if (!enemiesInSpawnArea[spawnPointIndex].Contains(enemy))
        {
            enemiesInSpawnArea[spawnPointIndex].Add(enemy);
        }
    }

    public void UnregisterEnemyFromSpawnArea(int spawnPointIndex, Collider2D enemy)
    {
        if (enemiesInSpawnArea[spawnPointIndex].Contains(enemy))
        {
            enemiesInSpawnArea[spawnPointIndex].Remove(enemy);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;
        if (skipSpawn)
        {
            skipSpawn = false;
            return;
        }
        if (Random.value < chanceToNotSpawn)
        {
            spawnIndex = (spawnIndex + 1) % spawnPoints.Length;  // Alternate even if no spawn
            return;
        }

        if (enemiesInSpawnArea[spawnIndex].Count > 0)
        {
            // If there's already an enemy in this spawn point, skip spawning
            spawnIndex = (spawnIndex + 1) % spawnPoints.Length;
            return;
        }

        Transform selectedSpawn = spawnPoints[spawnIndex];
        spawnIndex = (spawnIndex + 1) % spawnPoints.Length;  // Alternate for next spawn

        Vector3 spawnPosition = selectedSpawn.position;
        spawnPosition.y += Random.Range(-spawnOffset, spawnOffset);

        // Determine which enemy to spawn based on probabilities
        int randomIndex = 0; // Default to enemy[0]
        float randomValue = Random.value;
        Debug.Log(randomValue);

        if (randomValue < chanceEnemy2)
        {
            randomIndex = 1; // Spawn enemy[2] with 20% chance
        }
        if (randomValue < chanceEnemy3)
        {
            randomIndex = 2; // Spawn enemy[3] with 10% chance
        }
        var enemy = Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
        if (randomIndex > 1)
        {
            skipSpawn = true;
        }
        enemy.transform.SetParent(_enemies.transform);
    }

    void OnDrawGizmos()
    {
        if (!showSpawnGizmos || spawnPoints == null) return;

        Gizmos.color = Color.red;
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Gizmos.DrawWireSphere(point.position, 0.5f);
            }
        }
    }

    public void UpdateBPM(float newBPM)
    {
        bpm = newBPM;
        CalculateSpawnInterval();
        timeSinceLastSpawn = 0f;
    }
}
