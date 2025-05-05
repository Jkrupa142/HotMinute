using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Settings")]
    public Slider healthSlider; 
    public Image screenEdgeImage; 

    [Header("Death Settings")]
    public string gameOverSceneName = "GameOver"; 

    private Dictionary<string, float> activeDamageSources = new Dictionary<string, float>();

    private float totalDamagePerSecond = 0f;
    private float regenerationRate = 10f; 

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (screenEdgeImage != null)
        {
            screenEdgeImage.color = new Color(1f, 0f, 0f, 0f);
        }
    }

    void Update()
    {
        if (isDead)
            return;

        if (totalDamagePerSecond > 0f)
        {
            TakeDamage(totalDamagePerSecond * Time.deltaTime);
        }
        else
        {
            RegenerateHealth(regenerationRate * Time.deltaTime);
        }

        UpdateScreenEdges();
    }

    public void AddDamageSource(string sourceID, float damage)
    {
        if (!activeDamageSources.ContainsKey(sourceID))
        {
            activeDamageSources.Add(sourceID, damage);
            RecalculateTotalDamage();
        }
    }

    public void RemoveDamageSource(string sourceID)
    {
        if (activeDamageSources.ContainsKey(sourceID))
        {
            activeDamageSources.Remove(sourceID);
            RecalculateTotalDamage();
        }
    }

    private void RecalculateTotalDamage()
    {
        totalDamagePerSecond = 0f;
        foreach (var damage in activeDamageSources.Values)
        {
            totalDamagePerSecond += damage;
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0f && !isDead)
        {
            Die();
        }
    }


    private void RegenerateHealth(float regen)
    {
        currentHealth += regen;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }


    private void UpdateScreenEdges()
    {
        if (screenEdgeImage != null)
        {
            float healthRatio = currentHealth / maxHealth;
            float edgeAlpha = Mathf.Lerp(0f, 1f, 1f - healthRatio);
            screenEdgeImage.color = new Color(1f, 0f, 0f, edgeAlpha * 0.5f); 
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player Died!");
        if (FadeController.Instance != null)
        {
            FadeController.Instance.FadeToScene(gameOverSceneName);
        }
        else
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
