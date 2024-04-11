using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHandle : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    [SerializeField] private Monster monster;
    public event Action OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    
    public void TakeDamage(int amount)
    {
        //monster.animator.SetTrigger("Damage");
        monster.DamageAnimation();
        currentHealth -= amount;
        if (currentHealth <= 0)
        { Death(); }
    }


    
    void Death()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
