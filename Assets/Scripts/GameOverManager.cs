using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject telaGameOver;
    public AudioSource gameOverAudio;
    public AudioSource musicaFundo;

    void Start()
    {
        telaGameOver.SetActive(false);
    }

    public void AtivarGameOver()
    {
        telaGameOver.SetActive(true);

        // Para música de fundo
        if (musicaFundo != null)
        {
            musicaFundo.Stop();
        }

        // Toca música de Game Over
        if (gameOverAudio != null)
        {
            gameOverAudio.Play();
        }

        Time.timeScale = 0f;
    }
}