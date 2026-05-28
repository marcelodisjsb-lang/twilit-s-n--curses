using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Método chamado ao receber dano
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        // GAME OVER
        if (currentHealth <= 0)
        {
            FindObjectOfType<GameOverManager>().AtivarGameOver();
        }
    }

    // Método para curar o jogador
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}