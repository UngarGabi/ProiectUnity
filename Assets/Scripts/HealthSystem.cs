using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float amountHealth;
    [SerializeField] private float maxHealth;
    private bool isAlive = true;
    [SerializeField] private Slider healthBar;
    void Start()
    {
        amountHealth = maxHealth;
    }

    void Update()
    {
        UpdateUI();
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
        if (amountHealth + amount >= maxHealth)
        {
            amountHealth = maxHealth;
        }
        else
        {
            amountHealth += amount;
        }
        
    }

    private void UpdateUI()
    {
        if (healthBar == null)
            return;

        healthBar.value = amountHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Enemy"))
            EnemySpawner.maxNumberOfSpawns--;
    }
}
