using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    // Références pour le tir
    public GameObject bulletPrefab;
    public Transform gunMuzzle;
    public float shootingForce = 20f;
    public AudioSource audioSource;
    public AudioClip shootingSound;

    // Hitmarker UI
    public Image hitmarkerUI;
    public float hitmarkerDuration = 0.2f; // Temps d'affichage du hitmarker

    private void Update()
    {
        // Vérifier si on vise un ennemi
        CheckForEnemy();
    }

    public void FireWeapon()
    {
        // Vérifier si le prefab et le muzzle sont définis
        if (bulletPrefab != null && gunMuzzle != null)
        {
            // Créer le projectile (balle)
            GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Appliquer une force à la balle
            if (bulletRb != null)
            {
                bulletRb.AddForce(gunMuzzle.forward * shootingForce, ForceMode.Impulse);
            }

            // Jouer un son de tir
            if (audioSource != null && shootingSound != null)
            {
                audioSource.PlayOneShot(shootingSound);
            }
        }
    }

    private void CheckForEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunMuzzle.position, gunMuzzle.forward, out hit, 100f))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                ShowHitmarker();
            }
        }
    }

    private void ShowHitmarker()
    {
        if (hitmarkerUI != null)
        {
            hitmarkerUI.enabled = true;
            StopAllCoroutines();
            StartCoroutine(HideHitmarker());
        }
    }

    private IEnumerator HideHitmarker()
    {
        yield return new WaitForSeconds(hitmarkerDuration);
        if (hitmarkerUI != null)
        {
            hitmarkerUI.enabled = false;
        }
    }

    // Méthode appelée lors de la destruction de l'objet
    private void OnDestroy()
    {
        bulletPrefab = null;
        gunMuzzle = null;
        audioSource = null;
        shootingSound = null;
        hitmarkerUI = null;
    }
}
