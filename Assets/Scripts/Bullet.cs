using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // Dégâts infligés

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // Détruit la balle après impact
        }
    }
}