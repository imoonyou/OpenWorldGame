using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int currentHealth = 3;
    //private int maxHealth = 3;
    public event Action OnBossDeath;

    public void BossTakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        { BossDeath(); }
    }
    void BossDeath()
    {
        OnBossDeath?.Invoke();
        Destroy(gameObject);
    }
}
