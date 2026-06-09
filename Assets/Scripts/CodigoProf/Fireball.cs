using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float dano = 10;
    public float speed = 20f; // Velocidade da bala
    public float lifetime = 2f; // Tempo de vida da bala antes de ser destruída

    private void Start()
    {
        // Destrói a bala após um certo tempo
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move a bala na direção em que ela está apontando
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Inimigos normais
        if (other.CompareTag("Inimigo"))
        {
            SistemaDeVidaInimigo sistVida = other.GetComponent<SistemaDeVidaInimigo>();

            if (sistVida != null)
            {
                sistVida.AplicarDano(dano);
            }

            Destroy(gameObject);
            AudioManager.Instance.Play("Morte");
            return;
        }

        // Boss
        BossVida boss = other.GetComponent<BossVida>();

        if (boss != null)
        {
            boss.TakeDamage((int)dano);

            Destroy(gameObject);
            AudioManager.Instance.Play("Morte");
        }
    }
}
