using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerBody; // R�f�rence au corps du joueur (g�n�ralement le GameObject qui contient ton mod�le de personnage)
    public float mouseSensitivity = 100f; // Sensibilit� de la souris
    private float xRotation = 0f; // Rotation autour de l'axe X (haut/bas)

    void Start()
    {
        // Cache le curseur de la souris au d�but du jeu et le garde dans la fen�tre
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Lecture de l'entr�e de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Appliquer la rotation horizontale � la cam�ra (autour de l'axe Y)
        transform.Rotate(Vector3.up * mouseX);

        // Appliquer la rotation verticale � la cam�ra, mais limiter l'axe X (haut/bas)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite la rotation sur l'axe X entre -90 et 90 degr�s
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Appliquer la rotation horizontale au corps du joueur (autour de l'axe Y)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}