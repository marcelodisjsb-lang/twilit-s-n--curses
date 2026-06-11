using UnityEngine;

public class BossFightRain : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform player;
    [Header("Controle Chuva")]
    public float alturaSpawn = 10f;
    public int quantidade = 5;
    public float distancia = 4f;

    // Esta função será chamada pelo Animation Event
    public void ChuvaDeFogo()
    {
        for (int i = 0; i < quantidade; i++)
        {
            float x = player.position.x + Random.Range(-distancia, distancia);

            Vector3 pos = new Vector3(
                x,
                player.position.y + alturaSpawn,
                0f
            );

            Instantiate(fireballPrefab, pos, Quaternion.identity);
        }
    }
}