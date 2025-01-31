using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // D�g�ts inflig�s
    private RaycastHit hit; // D�claration de hit pour �viter une r�allocation inutile � chaque appel


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

    // M�thode appel�e lors de la destruction de l'objet
    private void OnDestroy()
    {
        // Nettoyage explicite si n�cessaire
        hit = default(RaycastHit); // R�initialisation de la variable hit
    }

}