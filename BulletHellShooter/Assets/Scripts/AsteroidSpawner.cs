using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public int numberOfSpawnPoints = 3;

    public BossSequenceManager bossSequenceManager;

    private float timer;
    private int spawnCount = 0;
    private const int maxSpawns = 40;
    private bool bossSequenceTriggered = false;

    void Update()
    {
        if (spawnCount >= maxSpawns)
        {
            if (!bossSequenceTriggered)
            {
                TriggerBossSequence();
            }
            return;
        }

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnAsteroids();
            spawnCount++;
        }
    }

    void SpawnAsteroids()
    {
        int pointsToPick = Mathf.Min(numberOfSpawnPoints, spawnPoints.Length);

        // Shuffle the spawn points and pick a subset
        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPoints);
        Shuffle(shuffledSpawnPoints);
        List<Transform> selectedSpawnPoints = shuffledSpawnPoints.GetRange(0, pointsToPick);

        if (asteroidPrefabs.Length > 0)
        {
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

            foreach (Transform spawnPoint in selectedSpawnPoints)
            {
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPoint.position, Quaternion.identity);

                // Assign a random rotation to the asteroid
                float randomAngle = Random.Range(-45f, 45f);
                asteroid.transform.rotation = Quaternion.Euler(0, 0, randomAngle);

                AsteroidMovement movement = asteroid.GetComponent<AsteroidMovement>();
                if (movement != null)
                {
                    movement.SetMovementDirection(randomAngle);
                }

                // Increment asteroid counter
                AsteroidCounter.Instance.IncrementAsteroidCount();
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void TriggerBossSequence()
    {
        if (bossSequenceManager != null)
        {
            bossSequenceManager.TriggerBossSequence();
            bossSequenceTriggered = true;
        }
    }
}