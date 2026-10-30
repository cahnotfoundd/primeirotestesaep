using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using NUnit.Framework;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private float damageCooldown = 1f;

    [Header("Interface")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private float nextDamageTime;
    private bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        if (isDead || amount <= 0)
        return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        if (isDead || Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + damageCooldown;
        TakeDamage(contactDamage);
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = currentHealth + "/" + maxHealth;
        }
    }

    private void Die ()
    {
        isDead = true;
        Debug.Log("Game Over!");

        // conectar tela de gameover depois 
    }
}
