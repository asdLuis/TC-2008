using UnityEngine;
using System.Collections;

public class BossAttackSequence : MonoBehaviour
{
    public GameObject bossObject;
    public GameObject asteroidPrefab;
    public int initialNumberOfLines = 5;
    public int linesIncreaseAmount = 3;
    public int asteroidsPerLine = 20;
    public float timeBetweenAsteroids = 0.2f;
    public float asteroidSpeed = 5f;
    public float radius = 2f;

    private Vector3 bossPosition;
    private int numberOfLines;
    private int attackCount = 0;
    private int maxAttackSequences = 5;

    void Start()
    {
        numberOfLines = initialNumberOfLines;
        UpdateBossPosition();
    }

    private void UpdateBossPosition()
    {
        bossPosition = bossObject.transform.position;
    }

    public void LaunchAsteroidSequence()
    {
        UpdateBossPosition();
        StartCoroutine(LaunchAllAsteroids());
    }

    private IEnumerator LaunchAllAsteroids()
    {
        if (attackCount >= maxAttackSequences)
        {
            yield break;
        }

        float angleStep = 360f / numberOfLines;

        for (int line = 0; line < numberOfLines; line++)
        {
            float angle = line * angleStep;
            StartCoroutine(LaunchAsteroidLine(angle));
        }

        yield return new WaitForSeconds(timeBetweenAsteroids * asteroidsPerLine);
        attackCount++;

        if (attackCount % 1 == 0)
        {
            numberOfLines += linesIncreaseAmount * 2;
        }
    }

    private IEnumerator LaunchAsteroidLine(float angle)
    {
        for (int i = 0; i < asteroidsPerLine; i++)
        {
            LaunchSingleAsteroid(angle);
            yield return new WaitForSeconds(timeBetweenAsteroids);
        }
    }

    private void LaunchSingleAsteroid(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.back;
        Vector3 spawnPosition = bossPosition + direction * radius;

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        BossAsteroidCounter.Instance.IncrementBossAsteroidCount();

        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * asteroidSpeed;
        }

        // Use a coroutine to delay the destruction and decrement
        StartCoroutine(DestroyAsteroidAfterDelay(asteroid, 10f));
    }

    private IEnumerator DestroyAsteroidAfterDelay(GameObject asteroid, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (asteroid != null)
        {
            BossAsteroidCounter.Instance.DecrementBossAsteroidCount();
            Destroy(asteroid);
        }
    }
}
