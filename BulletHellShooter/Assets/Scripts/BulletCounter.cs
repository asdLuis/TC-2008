using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCounterText;
    private int activeBullets;

    void Start()
    {
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
        if (activeBullets < 0) activeBullets = 0;
        UpdateBulletCounter();
    }
}
