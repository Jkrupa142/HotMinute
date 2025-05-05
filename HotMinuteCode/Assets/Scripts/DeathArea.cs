using UnityEngine;

public class DeathArea : MonoBehaviour
{
    [Header("Health Settings")]
    public float damagePerSecond = 10f; 
    private bool playerInside = false;
    private PlayerHealth playerHealth;

    private string damageSourceID;

    void Start()
    {
        damageSourceID = gameObject.name + "_" + GetInstanceID();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.AddDamageSource(damageSourceID, damagePerSecond);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on Player!");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (playerHealth != null)
            {
                playerHealth.RemoveDamageSource(damageSourceID);
            }
        }
    }

    void OnDestroy()
    {
        
        if (playerInside && playerHealth != null)
        {
            playerHealth.RemoveDamageSource(damageSourceID);
        }
    }
}
