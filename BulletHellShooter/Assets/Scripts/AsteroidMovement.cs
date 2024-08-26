using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed = 5f; // Speed at which the asteroid moves
    private Vector3 movementDirection;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    void Start()
    {
        // Calculate the boundaries based on the camera's position and size
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        minX = -cameraWidth;
        maxX = cameraWidth;
        minZ = -cameraHeight;
        maxZ = cameraHeight;
    }

    void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (transform.position.x < minX || transform.position.x > maxX ||
            transform.position.z < minZ || transform.position.z > maxZ)
        {
            DestroyAsteroid(); // Handle out-of-bounds
        }
    }

    public void SetMovementDirection(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        movementDirection = new Vector3(-Mathf.Sin(radian), 0, -Mathf.Cos(radian));
    }

    void DestroyAsteroid()
    {
        AsteroidCounter asteroidCounter = FindObjectOfType<AsteroidCounter>();
        if (asteroidCounter != null)
        {
            AsteroidCounter.Instance.DecrementAsteroidCount();
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyAsteroid(); // Destroy asteroid on collision with player
        }
    }
}