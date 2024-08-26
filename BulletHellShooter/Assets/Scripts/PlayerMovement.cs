using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float slowSpeed = 5f;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    void Start()
    {
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
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? slowSpeed : speed;

        float moveX = Input.GetAxis("Vertical");
        float moveZ = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveZ, 0f, moveX);

        Vector3 newPosition = transform.position + movement * currentSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
}
