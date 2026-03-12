using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SistemaMoedas))]
public class HUDController : MonoBehaviour
{
    public Image barraDeVida;
    public Sprite[] imagensBarraDeVida; // Array para armazenar as imagens dos coracoes
    public bool jogoPausado { get; private set; }

    [SerializeField] private int currentHealth = 0;
    private SistemaMoedas sistemaMoedas;
    private MenuPausa menuPausa;

    public static HUDController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        sistemaMoedas = GetComponent<SistemaMoedas>();
        menuPausa = GetComponentInChildren<MenuPausa>(includeInactive: true);
        UpdateHearts();
    }

    // M�todo que atualiza a interface dos coracoes
    public void UpdateHearts()
    {
        currentHealth = PlayerController.Instance.health.currentHealth;
        if (currentHealth >= 0 && currentHealth < imagensBarraDeVida.Length)
        {
            barraDeVida.sprite = imagensBarraDeVida[currentHealth];
        }
    }

    public void ColetarMoeda(int valor)
    {
        sistemaMoedas.GanharMoedas(valor);
    }
    public bool GastarMoedas(int custo)
    {
        if (custo > sistemaMoedas.moedas)
        {
            return false;
        }
        else
        {
            sistemaMoedas.moedas -= custo;
            sistemaMoedas.AtualizarUIMoedas();
            return true;
        }
    }

    public void PausarJogo()
    {
        jogoPausado = true;
        Time.timeScale = 0;
        menuPausa.gameObject.SetActive(true);
    }

    public void RetornarJogo()
    {
        jogoPausado = false;
        Time.timeScale = 1;
        menuPausa.gameObject.SetActive(false);
    }
}
