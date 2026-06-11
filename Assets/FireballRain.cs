using UnityEngine;

public class FireballRain : MonoBehaviour
{
    public float fallSpeed = 8f;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
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