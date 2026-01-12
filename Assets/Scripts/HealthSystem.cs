using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float amountHealth;
    [SerializeField] private float maxHealth;
    private bool isAlive = true;
    void Start()
    {
        amountHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        if (!isAlive) return;
        if (amount <= 0f) return;

        amountHealth -= amount;

        if (amountHealth <= 0)
        {
            amountHealth = 0f;
            isAlive = false;
            Die();
        }
        Debug.Log("-10");
    }

    public void Heal(float amount)
    {
        if (amountHealth + amount <= maxHealth)
        {
            amountHealth = maxHealth;
        }
        else
        {
            amountHealth += amount;
        }
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
