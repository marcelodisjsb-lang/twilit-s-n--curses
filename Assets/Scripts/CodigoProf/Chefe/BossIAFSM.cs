using System.Collections;
using UnityEngine;

public class BossIAFSM : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float speed = 3f;

    [Header("Attack Range")]
    public float attackRange = 5f;

    [Header("Fireball")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireCooldown = 4f;

    private bool canShoot = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        FacePlayer();
        TryShootFireball();
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (player == null) return;

        float distance = Mathf.Abs(player.position.x - transform.position.x);

        if (distance <= attackRange)
        {
            Debug.Log("Distância atual: " + distance);
            rb.velocity = Vector2.zero;
            return;
        }

        float direction = player.position.x > transform.position.x ? 1f : -1f;

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void FacePlayer()
    {
        if (sr == null || player == null) return;

        sr.flipX = player.position.x > transform.position.x;
    }

    void TryShootFireball()
    {
        if (!canShoot) return;
        if (player == null || fireballPrefab == null || firePoint == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        Debug.Log("Distância: " + distance);

        if (distance <= attackRange)
        {
            StartCoroutine(ShootFireball());
        }
    }

    IEnumerator ShootFireball()
    {
        Debug.Log("ATIROU");

        canShoot = false;

        Vector3 spawnPos = firePoint.position + firePoint.right * 0.3f;

        GameObject fb = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

        BossFireball bf = fb.GetComponent<BossFireball>();

        if (bf == null)
        {
            Debug.LogError("BossFireball não encontrado no prefab!");
        }
        else
        {
            Vector2 fireDirection = player.position.x > firePoint.position.x
                ? Vector2.right
                : Vector2.left;

            bf.SetDirection(fireDirection);
        }

        yield return new WaitForSeconds(fireCooldown);

        canShoot = true;
    }

    private bool canDamage = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null && canDamage)
        {
            player.TakeDamage(1);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}