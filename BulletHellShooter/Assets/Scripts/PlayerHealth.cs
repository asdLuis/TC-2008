using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public TextMeshProUGUI healthText;

    void Start()
    {
        UpdateHealthText();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage() // Changed to public
    {
        health--;
        UpdateHealthText();
        if (health <= 0)
        {
            // Handle player death
            Destroy(gameObject);
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }
}
