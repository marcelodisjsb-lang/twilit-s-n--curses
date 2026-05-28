using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarMenuCreditos : MonoBehaviour
{
    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }
}