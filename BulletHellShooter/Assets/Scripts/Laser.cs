using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    private BulletCounter bulletCounter;

    void Start()
    {
        Rigidbody laserRb = GetComponent<Rigidbody>();
        bulletCounter = FindObjectOfType<BulletCounter>();

        if (laserRb)
        {
            laserRb.velocity = transform.up * speed;
        }
    }

    void Update()
    {
        if (transform.position.x < -9f || transform.position.x > 9f ||
            transform.position.z < -5f || transform.position.z > 5f)
        {
            DestroyLaser();
        }
    }

    private void DestroyLaser()
    {
        if (bulletCounter != null)
        {
            bulletCounter.DecrementBulletCount();
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (bulletCounter != null)
        {
            bulletCounter.DecrementBulletCount();
        }
    }
}
