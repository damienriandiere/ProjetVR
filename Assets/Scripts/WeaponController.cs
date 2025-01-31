using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : MonoBehaviour
{

    // R�f�rences pour le tir
    public GameObject bulletPrefab;
    public Transform gunMuzzle;
    public float shootingForce = 20f;
    public AudioSource audioSource;
    public AudioClip shootingSound;
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
}
