using UnityEngine;

public class BossRisada : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play("RisadaBoss");
    }
}