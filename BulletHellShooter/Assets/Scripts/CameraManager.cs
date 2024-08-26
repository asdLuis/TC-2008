using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static float minX;
    public static float maxX;
    public static float minZ;
    public static float maxZ;

    void Awake()
    {
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        minX = -cameraWidth;
        maxX = cameraWidth;
        minZ = -cameraHeight;
        maxZ = cameraHeight;
    }
}
