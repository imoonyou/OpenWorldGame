using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    public Rigidbody body;
    public HealthBar healthBar;

    public string Respawn;
    

    private static int deathCount = 0; // Static variable to store the death count
    public TextMeshProUGUI deathCountText;
  

    void Awake()
    {
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    private void Start()
    {
        
        UpdateDeathCount();
        healthBar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
        if (currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        deathCount++;
        SceneManager.LoadScene(Respawn);
        UpdateDeathCount();
    }

    public void ApplyKnockback(Vector3 knockbackDirection)
    {
        // Normalize the direction vector to ensure consistent force regardless of its length
        knockbackDirection.Normalize();

        // Apply force in the opposite direction of knockbackDirection
        body.AddForce(-knockbackDirection * 50f, ForceMode.Impulse);
    }

    public void UpdateDeathCount()
    {
        deathCountText.text = deathCount.ToString();
    }
}
