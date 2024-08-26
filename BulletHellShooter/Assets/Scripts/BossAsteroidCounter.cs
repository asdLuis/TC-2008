using UnityEngine;
using TMPro;

public class BossAsteroidCounter : MonoBehaviour
{
    public TextMeshProUGUI bossAsteroidCounterText;
    private int bossAsteroidCount = 0;

    public static BossAsteroidCounter Instance { get; private set; }

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
        UpdateBossAsteroidCounter();
    }

    public void IncrementBossAsteroidCount()
    {
        bossAsteroidCount++;
        UpdateBossAsteroidCounter();
    }

    public void DecrementBossAsteroidCount()
    {
        bossAsteroidCount--;
        if (bossAsteroidCount < 0) bossAsteroidCount = 0;
        UpdateBossAsteroidCounter();
    }

    private void UpdateBossAsteroidCounter()
    {
        if (bossAsteroidCounterText != null)
        {
            bossAsteroidCounterText.text = "Boss Asteroids: " + bossAsteroidCount;
        }
    }
}