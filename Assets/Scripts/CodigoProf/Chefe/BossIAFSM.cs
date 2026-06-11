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

    [Header("Chuva")]
    public float chuvaCooldown = 5f;

    [Header("Boss")]
    public float cooldownBoss = 2f;
    private bool podeAtacar = false;

    private bool canShoot = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BossFightRain bossFightRain;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bossFightRain = GetComponent<BossFightRain>();
        StartCoroutine(DestravarBoss());
    }

    void Update()
    {        
        if (!podeAtacar) { return; }

        FacePlayer();
        TryShootFireball();
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    IEnumerator DestravarBoss()
    {
        yield return new WaitForSeconds(cooldownBoss);
        podeAtacar=true;
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
        else
        {
            StartCoroutine(TryCastRain());
        }
    }

    IEnumerator ShootFireball()
    {

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

    IEnumerator TryCastRain()
    {
        canShoot = false;
        bossFightRain.ChuvaDeFogo();

        yield return new WaitForSeconds(chuvaCooldown);

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