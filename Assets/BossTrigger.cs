using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossIAFSM bossFSM;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossFSM != null)
            {
                bossFSM.enabled = true;
            }
            else
            {
                Debug.LogError("BossFSM não foi atribuído no Inspector!");
            }

            Destroy(gameObject);
        }
    }
}