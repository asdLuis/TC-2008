using UnityEngine;
using TMPro;

public class AsteroidCounter : MonoBehaviour
{
    public TextMeshProUGUI asteroidCounterText; // Reference to your TextMeshPro counter display
    private int asteroidCount = 0;

    // Singleton instance
    public static AsteroidCounter Instance { get; private set; }

    void Awake()
    {
        // Ensure only one instance of AsteroidCounter exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this); // Only destroy this component, not the entire GameObject
        }
    }

    void Start()
    {
        UpdateAsteroidCounter();
    }

    public void IncrementAsteroidCount()
    {
        asteroidCount++;
        UpdateAsteroidCounter();
    }

    public void DecrementAsteroidCount()
    {
        asteroidCount--;
        if (asteroidCount < 0) asteroidCount = 0; // Prevent negative counts
        UpdateAsteroidCounter();
    }

    private void UpdateAsteroidCounter()
    {
        if (asteroidCounterText != null)
        {
            asteroidCounterText.text = "Asteroids: " + asteroidCount;
        }
    }
}