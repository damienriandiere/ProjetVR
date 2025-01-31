using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameTime = 60f; // Dur�e du jeu en secondes
    private float timeRemaining;
    private bool gameEnded = false;

    public TextMeshProUGUI timerText; // R�f�rence au texte affichant le temps
    public Enemy[] enemies; // Tableau des ennemis

    void Start()
    {
        timeRemaining = gameTime;
    }

    void Update()
    {
        if (gameEnded) return; // Ne plus mettre � jour apr�s la fin du jeu

        timeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        if (timeRemaining <= 0)
        {
            timerText.text = "Temps �coul� !";
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Temps restant: " + Mathf.Ceil(timeRemaining) + "s";
    }

    public void EndGame()
    {
        gameEnded = true;

        // V�rifier si tous les ennemis sont morts
        bool allEnemiesDead = true;
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null) // Si un ennemi existe encore, il n'est pas mort
            {
                allEnemiesDead = false;
                break;
            }
        }

        if (allEnemiesDead)
        {
            Debug.Log("Vous avez gagn� !");
        }
        else
        {
            Debug.Log("Vous avez perdu !");
        }

        // Quitter le jeu apr�s 3 secondes
        Invoke("QuitGame", 3f);
    }

    void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // M�thode appel�e lors de la destruction de l'objet
    private void OnDestroy()
    {
        // Nettoyer les r�f�rences pour �viter des fuites m�moire
        timerText = null;
        enemies = null;
    }
}
