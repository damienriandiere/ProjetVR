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

    private Animator anim; // Référence à l'Animator
    private bool isDead = false; // Vérifie si l'ennemi est déjà mort

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator non trouvé sur " + gameObject.name);
        }
    }

    private void Update()
    {
        // Toujours face à la caméra
        if (healthBarCanvas != null)
        {
            healthBarCanvas.LookAt(Camera.main.transform);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
            Destroy(collision.gameObject);
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
        isDead = true; // L'ennemi est mort
        anim.SetBool("isDead", isDead); // Déclenche l'animation de mort
        Debug.Log(anim.GetBool("isDead"));

        GetComponent<Collider>().enabled = false; // Désactive les collisions
        GetComponent<Rigidbody>().isKinematic = true; // Évite les réactions physiques

        // Détruit l’ennemi après l'animation
        Destroy(gameObject, 3f); // Laisse le temps à l'animation de jouer
    }

    // Méthode appelée lors de la destruction de l'objet
    private void OnDestroy()
    {
        // Nettoyer les références pour éviter des fuites mémoire
        healthBarFill = null;
        healthBarCanvas = null;
        anim = null;
    }
}
