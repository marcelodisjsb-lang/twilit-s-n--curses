using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject telaGameOver;

    private bool jogoPausado;

    public void PausarJogo()
    {
        jogoPausado = true;

        Time.timeScale = 0;

        menuPausa.SetActive(true);
    }

    public void RetornarJogo()
    {
        jogoPausado = false;

        Time.timeScale = 1;

        menuPausa.SetActive(false);
    }

    public void AbrirGameOver()
    {
        jogoPausado = true;

        Time.timeScale = 0;

        telaGameOver.SetActive(true);
    }

    public void Sair()
    {
        Debug.Log("Sair do jogo");

        Application.Quit();
    }
}