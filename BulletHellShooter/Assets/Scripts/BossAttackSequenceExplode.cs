using UnityEngine;
using System.Collections;

public class BossAttackSequenceExplode : MonoBehaviour
{
    public GameObject bigAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public float bigAsteroidSpeed = 1f;
    public float smallAsteroidSpeed = 12f;
    public float explosionDuration = 2f;

    public void LaunchExplodeSequence()
    {
        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        // Spawn the big asteroid
        GameObject bigAsteroid = Instantiate(bigAsteroidPrefab, transform.position, Quaternion.identity);
        AsteroidCounter.Instance.IncrementAsteroidCount();
        Rigidbody bigRb = bigAsteroid.GetComponent<Rigidbody>();
        if (bigRb != null)
        {
            bigRb.velocity = Vector3.back * bigAsteroidSpeed;
        }

        yield return new WaitForSeconds(explosionDuration);

        BossAsteroidCounter.Instance.DecrementBossAsteroidCount();
        Destroy(bigAsteroid);

        // Spawn small asteroids in all directions
        for (int i = 0; i < 360; i += 15)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 spawnPosition = transform.position;

            GameObject smallAsteroid = Instantiate(smallAsteroidPrefab, spawnPosition, Quaternion.identity);
            BossAsteroidCounter.Instance.IncrementBossAsteroidCount();
            Rigidbody smallRb = smallAsteroid.GetComponent<Rigidbody>();
            if (smallRb != null)
            {
                smallRb.velocity = direction * smallAsteroidSpeed;
            }
            BossAsteroidCounter.Instance.DecrementBossAsteroidCount();
            StartCoroutine(DestroyAsteroidAfterDelay(smallAsteroid, 5f));
        }
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
