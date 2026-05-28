using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject telaGameOver;

    void Start()
    {
        telaGameOver.SetActive(false);
    }

    public void AtivarGameOver()
    {
        telaGameOver.SetActive(true);

        Time.timeScale = 0f;
    }
}