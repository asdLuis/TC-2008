using UnityEngine;

public class Asteroid : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Decrease player health
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }

            // Decrement asteroid counter
            AsteroidCounter.Instance.DecrementAsteroidCount();

            // Destroy the asteroid
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Decrement asteroid counter if destroyed by other means (e.g., player shot)
        AsteroidCounter.Instance.DecrementAsteroidCount();
    }
}
