using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public float laserSpeed = 10f;
    public Transform firePoint;
    [SerializeField] private Vector3 laserRotation = Vector3.zero;

    private BulletCounter bulletCounter;

    void Start()
    {
        bulletCounter = FindObjectOfType<BulletCounter>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        if (laserPrefab && firePoint)
        {
            GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.Euler(laserRotation));
            Rigidbody laserRb = laser.GetComponent<Rigidbody>();
            if (laserRb)
            {
                laserRb.velocity = firePoint.up * laserSpeed;
            }

            if (bulletCounter != null)
            {
                bulletCounter.IncrementBulletCount();
            }
        }
    }
}
