using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCounterText;
    private int activeBullets;

    void Start()
    {
        if (bulletCounterText == null)
        {
            Debug.LogError("Bullet Counter Text is not assigned.");
        }
        UpdateBulletCounter();
    }

    void UpdateBulletCounter()
    {
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "Bullets: " + activeBullets;
        }
    }

    public void IncrementBulletCount()
    {
        activeBullets++;
        UpdateBulletCounter();
    }

    public void DecrementBulletCount()
    {
        activeBullets--;
        UpdateBulletCounter();
    }
}
