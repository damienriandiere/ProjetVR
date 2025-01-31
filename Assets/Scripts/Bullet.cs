using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // Dégâts infligés
    private RaycastHit hit; // Déclaration de hit pour éviter une réallocation inutile à chaque appel


    private void OnTriggerEnter(Collider other)
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    // Méthode appelée lors de la destruction de l'objet
    private void OnDestroy()
    {
        // Nettoyage explicite si nécessaire
        hit = default(RaycastHit); // Réinitialisation de la variable hit
    }

}