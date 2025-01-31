using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // D�g�ts inflig�s

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // D�truit la balle apr�s impact
        }
    }
}