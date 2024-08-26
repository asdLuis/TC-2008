using UnityEngine;
using System.Collections;

public class BossAttackSequenceSides : MonoBehaviour
{
    public GameObject bossObject;
    public GameObject asteroidPrefab;
    public int asteroidsPerLine = 20;
    public float timeBetweenAsteroids = 0.2f;
    public float asteroidSpeed = 5f;

    private Vector3 bossPosition;
    private int attackCount = 0;
    private int maxAttackSequences = 5;

    private float[] xBorders = { 9f, -9f };
    private float[] zBorders = { 5f, -5f };

    void Start()
    {
        UpdateBossPosition();
    }

    private void UpdateBossPosition()
    {
        bossPosition = bossObject.transform.position;
    }

    public void LaunchAsteroidFromBorders()
    {
        UpdateBossPosition();
        StartCoroutine(LaunchAsteroidsFromBorders());
    }

    private IEnumerator LaunchAsteroidsFromBorders()
    {
        if (attackCount >= maxAttackSequences)
        {
            yield break;
        }

        foreach (float xBorder in xBorders)
        {
            for (int i = 0; i < 3; i++)
            {
                float z = Random.Range(zBorders[0], zBorders[1]);
                Vector3 point = new Vector3(xBorder, 0, z);
                StartCoroutine(LaunchAsteroidLineFromPoint(point, Vector3.left));
            }
        }

        // Randomly pick 3 points on Z borders
        foreach (float zBorder in zBorders)
        {
            for (int i = 0; i < 3; i++)
            {
                float x = Random.Range(xBorders[0], xBorders[1]);
                Vector3 point = new Vector3(x, 0, zBorder);
                StartCoroutine(LaunchAsteroidLineFromPoint(point, Vector3.back));
            }
        }

        yield return new WaitForSeconds(timeBetweenAsteroids * asteroidsPerLine);

        attackCount++;
    }

    private IEnumerator LaunchAsteroidLineFromPoint(Vector3 startPoint, Vector3 direction)
    {
        for (int i = 0; i < asteroidsPerLine; i++)
        {
            LaunchSingleAsteroid(startPoint, direction);
            BossAsteroidCounter.Instance.IncrementBossAsteroidCount();
            yield return new WaitForSeconds(timeBetweenAsteroids);
        }
    }

    private void LaunchSingleAsteroid(Vector3 startPoint, Vector3 direction)
    {
        Vector3 spawnPosition = startPoint;

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * asteroidSpeed;
        }
        Destroy(asteroid, 10f);
        BossAsteroidCounter.Instance.DecrementBossAsteroidCount();
    }
}
