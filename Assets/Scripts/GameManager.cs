using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameTime = 60f; // Dur�e du jeu en secondes
    private float timeRemaining;
    private bool gameEnded = false;
    private bool isPaused = false;

    public Slider timeProgressBar; // R�f�rence au Slider pour la barre de progression
    public TextMeshProUGUI progressText; // R�f�rence au texte affichant le pourcentage
    public Enemy[] enemies; // Tableau des ennemis

    public GameObject pauseMenuCanvas; // R�f�rence au Canvas du menu de pause
    public TextMeshProUGUI helpText; // R�f�rence au texte d'aide

    public Button resumeButton; // R�f�rence au bouton Resume
    public Button helpButton; // R�f�rence au bouton Help
    public Button quitButton; // R�f�rence au bouton Quit
    public GameObject helpPanel; // R�f�rence au Panel du menu d'aide
    public Button backButton; // R�f�rence au bouton Retour

    void Start()
    {
        timeRemaining = gameTime;
        timeProgressBar.maxValue = gameTime;
        timeProgressBar.value = gameTime;

        // D�sactiver le menu de pause au d�marrage
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        // D�sactiver le menu d'aide au d�marrage
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
        }

        // Configurer les boutons
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (helpButton != null)
        {
            helpButton.onClick.AddListener(ShowHelp);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }

        // Configurer le bouton Retour
        if (backButton != null)
        {
            backButton.onClick.AddListener(HideHelp);
        }
    }

    void Update()
    {
        if (gameEnded) return; // Ne plus mettre � jour apr�s la fin du jeu

        // G�rer la pause avec la touche Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Mettre � jour le temps restant si le jeu n'est pas en pause
        if (!isPaused)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                progressText.text = "0%";
                EndGame();
            }
        }
    }

    void UpdateTimerUI()
    {
        // Mettre � jour la valeur du Slider
        timeProgressBar.value = timeRemaining;

        // Calculer le pourcentage de temps restant
        float percentage = (timeRemaining / gameTime) * 100f;
        progressText.text = Mathf.CeilToInt(percentage) + "%";
    }

    public void EndGame()
    {
        gameEnded = true;
        Debug.Log("Fin du jeu !");
    }

    // Fonction pour activer/d�sactiver la pause
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Activer le menu de pause
            pauseMenuCanvas.SetActive(true);
            // Arr�ter le temps
            Time.timeScale = 0f;
        }
        else
        {
            // D�sactiver le menu de pause
            pauseMenuCanvas.SetActive(false);
            // D�sactiver le texte d'aide
            helpText.gameObject.SetActive(false);
            // Reprendre le temps
            Time.timeScale = 1f;
        }
    }

    // Fonction pour reprendre le jeu
    public void ResumeGame()
    {
        TogglePause(); // D�sactive la pause
    }

    // Fonction pour afficher l'aide
    public void ShowHelp()
    {
        // Activer le menu d'aide
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);

        }
    }

    // Fonction pour masquer l'aide et revenir au menu de pause
    public void HideHelp()
    {
        // D�sactiver le menu d'aide
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
        }
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");
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
