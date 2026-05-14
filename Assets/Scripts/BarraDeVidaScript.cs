using UnityEngine;

public class BarraDeVidaScript : MonoBehaviour
{
    [Header("Sprites dos estágios da vida (do cheio ao vazio)")]
    public Sprite[] frames;

    [Header("Componente do sprite")]
    public SpriteRenderer spriteRenderer;

    [Header("Valor atual da vida (0 = vazio, 1 = cheio)")]
    [Range(0f, 1f)]
    public float vida = 1f;

    void Start()
    {
        AtualizarBarra();
    }

    void Update()
    {
        AtualizarBarra();
    }

    void AtualizarBarra()
    {
        if (frames.Length == 0 || spriteRenderer == null)
            return;

        // Calcula o índice com base na vida (inverte para começar cheio)
        int index = Mathf.Clamp(Mathf.RoundToInt((1 - vida) * (frames.Length - 1)), 0, frames.Length - 1);
        spriteRenderer.sprite = frames[index];
    }

    public void SetVida(float novaVida)
    {
        vida = Mathf.Clamp01(novaVida);
    }
}
