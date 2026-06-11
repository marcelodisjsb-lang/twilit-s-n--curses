using UnityEngine;
using UnityEngine.SceneManagement;

public class BossVida : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Boss tomou " + damage + " de dano!");
        Debug.Log("Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Boss morreu!");
            SceneManager.LoadScene("Creditos");
        }
    }
}