using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float amountHealth; // viata curenta player sau enemy
    [SerializeField] private float maxHealth; // viata maxima setata in inspector
    private bool isAlive = true;
    [SerializeField] private Slider healthBar; // Bara de HP
    void Start()
    {
        amountHealth = maxHealth; // dam hp-ul 
    }

    void Update()
    {
        UpdateUI();
    }

    public void TakeDamage(float amount) // functie care face ca gameobjectul sa ia dmg
    {
        // verificari 
        if (!isAlive) return;
        if (amount <= 0f) return;

        amountHealth -= amount; 

        if (amountHealth <= 0) // verificare death
        {
            amountHealth = 0f;
            isAlive = false;
            Die();
        }
    }

    public void Heal(float amount) // adauga hp
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

    private void Die() // functia care opreste gameobject-ul
    {
        if (gameObject.CompareTag("Enemy")) 
        {
            EnemySpawner.maxNumberOfSpawns--; // numarul de inamici pe harta(max 25) 
            ScoreTracker.Instance.AddKill(); // adauga un kill la scor
            Destroy(gameObject);

        }

        if (gameObject.CompareTag("Player"))
        {
            isAlive = false;

            // opreste gameplay-ul
            Time.timeScale = 0f;

            // dezactiveaza HP bar
            if (healthBar != null)
                healthBar.gameObject.SetActive(false);

            // calculeaza scorul
            int finalScore = ScoreTracker.Instance.CalculateScore_NoWinBonus();
            float time = ScoreTracker.Instance.GetTimePlayed();

            // afiseaza GameOver UI
            GameOverUI gameOverUI = FindObjectOfType<GameOverUI>();
            if (gameOverUI != null)
            {
                gameOverUI.ShowGameOver(finalScore, time);
            }
            else
            {
                Debug.LogError("GameOverUI nu exista in scena!");
            }

            // nu distrugem Player-ul (ca sa ramana UI + camera)
            return;
        }
    }
}
