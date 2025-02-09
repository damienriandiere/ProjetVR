using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    // R�f�rences pour le tir
    public GameObject bulletPrefab;
    public Transform gunMuzzle;
    public float shootingForce = 20f;
    public AudioSource audioSource;
    public AudioClip shootingSound;

    // Hitmarker UI
    public Image hitmarkerUI;

    private void Start()
    {
        // Afficher imm�diatement le hitmarker au d�marrage du jeu
        if (hitmarkerUI != null)
        {
            hitmarkerUI.enabled = true;
        }
    }
    public void FireWeapon()
    {
        // V�rifier si le prefab et le muzzle sont d�finis
        if (bulletPrefab != null && gunMuzzle != null)
        {
            // Cr�er le projectile (balle)
            GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Appliquer une force � la balle
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

    // M�thode appel�e lors de la destruction de l'objet
    private void OnDestroy()
    {
        bulletPrefab = null;
        gunMuzzle = null;
        audioSource = null;
        shootingSound = null;
        hitmarkerUI = null;
    }
}
