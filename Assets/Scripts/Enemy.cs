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
        // Toujours face à la caméra
        if (healthBarCanvas != null)
        {
            healthBarCanvas.LookAt(Camera.main.transform);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Mise à jour de la barre de vie
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        // Si l'ennemi n'a plus de vie, il est détruit
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Détruit l'ennemi
        FindObjectOfType<GameManager>().EndGame();
    }
}
