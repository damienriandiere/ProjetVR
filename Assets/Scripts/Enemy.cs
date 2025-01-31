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

    private Animator anim; // R�f�rence � l'Animator
    private bool isDead = false; // V�rifie si l'ennemi est d�j� mort

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator non trouv� sur " + gameObject.name);
        }
    }

    private void Update()
    {
        // Toujours face � la cam�ra
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
        isDead = true; // L'ennemi est mort
        anim.SetBool("isDead", isDead); // D�clenche l'animation de mort
        Debug.Log(anim.GetBool("isDead"));

        GetComponent<Collider>().enabled = false; // D�sactive les collisions
        GetComponent<Rigidbody>().isKinematic = true; // �vite les r�actions physiques

        // D�truit l�ennemi apr�s l'animation
        Destroy(gameObject, 3f); // Laisse le temps � l'animation de jouer
    }

    // M�thode appel�e lors de la destruction de l'objet
    private void OnDestroy()
    {
        // Nettoyer les r�f�rences pour �viter des fuites m�moire
        healthBarFill = null;
        healthBarCanvas = null;
        anim = null;
    }
}
