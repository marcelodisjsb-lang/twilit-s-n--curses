using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image vidaAtualImage;

    public void AtualizarUI(float calculoVida)
    {
        Vector3 novaEscala = new Vector3(calculoVida, 1, 1);
        vidaAtualImage.transform.localScale = novaEscala;
    }
}
