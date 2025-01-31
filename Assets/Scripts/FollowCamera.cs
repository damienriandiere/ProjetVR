using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;  // Le transform du joueur
    public float rotationSpeed = 5f;  // Vitesse de rotation

    void Update()
    {
        // Faire tourner le joueur uniquement sur l'axe Y en fonction de la cam�ra
        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        player.Rotate(0f, horizontalRotation, 0f);

        // Placer la cam�ra coll�e au joueur (ici on utilise la position du joueur + une petite d�calage)
        transform.position = player.position;

        // Faire en sorte que la cam�ra regarde toujours le joueur
        transform.LookAt(player);
    }

    // M�thode appel�e lors de la destruction de l'objet
    private void OnDestroy()
    {
        // R�initialiser les r�f�rences pour �viter des fuites m�moire
        player = null;
    }
}
