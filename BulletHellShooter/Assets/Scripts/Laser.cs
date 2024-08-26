using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        Rigidbody laserRb = GetComponent<Rigidbody>();
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
            Destroy(gameObject);
        }
    }
}
