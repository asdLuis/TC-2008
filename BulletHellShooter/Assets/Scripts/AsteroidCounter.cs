using UnityEngine;
using TMPro;

public class AsteroidCounter : MonoBehaviour
{
    public TextMeshProUGUI asteroidCounterText;
    private int asteroidCount = 0;

    public static AsteroidCounter Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this);
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
        if (asteroidCount < 0) asteroidCount = 0;
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