using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    public Rigidbody body;
    public HealthBar healthBar;

    public string Respawn;

    void Awake()
    {
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    private void Start()
    {
        
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
        //Destroy(gameObject);
        SceneManager.LoadScene(Respawn);
        // Death function
        // TEMPORARY: Destroy Object
    }

    public void ApplyKnockback(Vector3 knockbackDirection)
    {
        // Normalize the direction vector to ensure consistent force regardless of its length
        knockbackDirection.Normalize();

        // Apply force in the opposite direction of knockbackDirection
        body.AddForce(-knockbackDirection * 50f, ForceMode.Impulse);
    }
}
