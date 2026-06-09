using UnityEngine;

public class BossFireball : MonoBehaviour
{
    public float speed = 7f;
    public float lifeTime = 3f;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            sr.flipX = direction.x < 0;
        }

        Debug.Log("SET DIRECTION: " + direction);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Debug.Log("UPDATE: " + direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(1); // dano da fireball
            Destroy(gameObject);  // destrói a bola ao acertar
        }
    }
}