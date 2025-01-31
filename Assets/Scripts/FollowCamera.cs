using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;  // Le transform du joueur
    public float rotationSpeed = 5f;  // Vitesse de rotation

    void Update()
    {
        // Faire tourner le joueur uniquement sur l'axe Y en fonction de la caméra
        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        player.Rotate(0f, horizontalRotation, 0f);

        // Placer la caméra collée au joueur (ici on utilise la position du joueur + une petite décalage)
        transform.position = player.position;

        // Faire en sorte que la caméra regarde toujours le joueur
        transform.LookAt(player);
    }

    // Méthode appelée lors de la destruction de l'objet
    private void OnDestroy()
    {
        // Réinitialiser les références pour éviter des fuites mémoire
        player = null;
    }
}
