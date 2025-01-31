using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Vie de l'ennemi")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Barre de vie")]
    public Image healthBarFill; // Glisser l'image "Fill" ici
    public Transform healthBarCanvas; // Glisser le Canvas ici

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Toujours face � la cam�ra
        if (healthBarCanvas != null)
        {
            healthBarCanvas.LookAt(Camera.main.transform);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Mise � jour de la barre de vie
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        // Si l'ennemi n'a plus de vie, il est d�truit
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // D�truit l'ennemi
        FindObjectOfType<GameManager>().EndGame();
    }
}
