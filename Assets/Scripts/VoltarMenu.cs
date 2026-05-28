using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarMenu : MonoBehaviour
{
    public void MenuPrincipal()
    {
        // Volta o tempo ao normal
        Time.timeScale = 1f;

        // Carrega a cena do menu
        SceneManager.LoadScene("Menu");
    }
}