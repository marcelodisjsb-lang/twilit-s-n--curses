using UnityEngine;

public class BossPatrulha : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float patrolSpeed = 2f;

    private bool movingRight = true;
    private bool patrulhando = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // ✔ AQUI (FALTAVA ISSO)
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if (patrulhando)
        {
            Patrulhando();
        }

        // FLIP PROFISSIONAL AQUI
        if (rb.velocity.x > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (rb.velocity.x < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }

    public void Patrulhar(bool estado)
    {
        patrulhando = estado;
    }

    private void Patrulhando()
    {
        if (rightPoint == null || leftPoint == null) return;

        float targetX = movingRight ? rightPoint.position.x : leftPoint.position.x;

        float direction = targetX > transform.position.x ? 1f : -1f;

        rb.velocity = new Vector2(direction * patrolSpeed, rb.velocity.y);

        if (movingRight && transform.position.x >= rightPoint.position.x)
            movingRight = false;
        else if (!movingRight && transform.position.x <= leftPoint.position.x)
            movingRight = true;
    }
}