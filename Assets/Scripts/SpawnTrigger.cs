using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private EnemySpawner spawner;
    private int spawnPointIndex;

    public void Initialize(EnemySpawner spawner, int spawnPointIndex)
    {
        this.spawner = spawner;
        this.spawnPointIndex = spawnPointIndex;

        // Ensure the GameObject has a 2D collider set as a trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            spawner.RegisterEnemyInSpawnArea(spawnPointIndex, other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            spawner.UnregisterEnemyFromSpawnArea(spawnPointIndex, other);
        }
    }
}