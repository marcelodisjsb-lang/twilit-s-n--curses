using UnityEngine;
using UnityEngine.SceneManagement;

public class IrCreditos : MonoBehaviour
{
    public void AbrirCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}