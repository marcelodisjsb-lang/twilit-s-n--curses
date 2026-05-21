using System;
using System.Collections;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 2f;
    public int maxHealth = 2;
    public float knockbackForce = 5f;
    [SerializeField] bool movingRight = true;
    public int danoInimigo = 1;

    [Header("Pontos de Patrulha")]
    public Transform pontoA;
    public Transform pontoB;

    private bool vivo = true;
    private bool isKnockBacked = false;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private float targetX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Debug.Log(spriteRenderer);
    }

    void Update()
    {
        if (isKnockBacked || !vivo) return;

        Patrulhar();
    }

    void Patrulhar()
    {
        Transform alvo = movingRight ? pontoB : pontoA;

        // move suavemente até o alvo
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(alvo.position.x, transform.position.y),
            moveSpeed * Time.deltaTime
        );

        // virar sprite
        if (movingRight)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        // chegou no ponto?
        if (Mathf.Abs(transform.position.x - alvo.position.x) < 0.05f)
        {
            movingRight = !movingRight;
        }

        anim.SetFloat("Velocidade", moveSpeed);
    }

    private void MirrorSprite(float moveInput)
    {
        if (moveInput > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController sistemaVida = collision.gameObject.GetComponent<PlayerController>();

            if (sistemaVida != null)
            {
                sistemaVida.TakeDamage(danoInimigo);
            }
        }
    }

    public void EfeitoDeRecuo()
    {
        isKnockBacked = true;

        float knockbackDirection = movingRight ? -1 : 1;
        Vector2 force = new(knockbackDirection * knockbackForce, 0);

        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(ResetKnockback());
    }

    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.5f);
        isKnockBacked = false;
    }

    public void EfeitoDePiscar()
    {
        StartCoroutine(Piscar());
    }

    IEnumerator Piscar()
    {
        Color corOriginal = spriteRenderer.color;
        Color corTransparente = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 0.5f);

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = corTransparente;
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = corOriginal;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void AnimacaoDeDano()
    {
        anim.SetTrigger("Machucado");
        StartCoroutine(ResetMachucado());
    }

    IEnumerator ResetMachucado()
    {
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Machucado");
    }

    internal void AnimacaoDeMorte()
    {
        vivo = false;

        rb.isKinematic = true;
        col.enabled = false;

        anim.SetBool("Vivo", vivo);

        EfeitoDePiscar();

        Destroy(gameObject, 3);
    }
}