using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime = 60f; // Durée du jeu en secondes
    private float timeRemaining;
    private bool gameEnded = false;
    private bool isPaused = false;

    [Header("UI Elements")]
    public Slider timeProgressBar;
    public TextMeshProUGUI progressText;
    public GameObject pauseMenuCanvas;
    public TextMeshProUGUI helpText;
    public GameObject helpPanel;

    [Header("End Screens")]
    public GameObject defeatPanel;
    public GameObject victoryPanel;
    public Button restartButton;
    public Button quitButton;
    public Button restartButtonVictory;
    public Button quitButtonVictory;

    [Header("Buttons")]
    public Button resumeButton;
    public Button helpButton;
    public Button quitGameButton;
    public Button backButton;

    public AudioSource menuAudioSource;

    [Header("Game Elements")]
    public List<Enemy> enemies = new List<Enemy>(); // Liste des ennemis actifs

    [Header("Audio")]
    public AudioSource alloSelemAudio; // 🔊 Fichier "Allo Selem"

    void Start()
    {
        timeRemaining = gameTime;
        timeProgressBar.maxValue = gameTime;
        timeProgressBar.value = gameTime;

        // Désactiver les menus au démarrage
        pauseMenuCanvas?.SetActive(false);
        helpPanel?.SetActive(false);
        defeatPanel?.SetActive(false);
        victoryPanel?.SetActive(false); // ✅ Ajout du menu de victoire

        // Configurer les boutons
        if (resumeButton != null)
            resumeButton.onClick.AddListener(() => { PlayMenuSound(); ResumeGame(); });

        if (helpButton != null)
            helpButton.onClick.AddListener(() => { PlayMenuSound(); ShowHelp(); });

        if (quitGameButton != null)
            quitGameButton.onClick.AddListener(() => { PlayMenuSound(); QuitGame(); });

        if (backButton != null)
            backButton.onClick.AddListener(() => { PlayMenuSound(); HideHelp(); });

        if (restartButton != null)
            restartButton.onClick.AddListener(() => { PlayMenuSound(); RestartGame(); });

        if (quitButton != null)
            quitButton.onClick.AddListener(() => { PlayMenuSound(); QuitGame(); });

        if (restartButtonVictory != null)
            restartButtonVictory.onClick.AddListener(() => { PlayMenuSound(); RestartGame(); });

        if (quitButtonVictory != null)
            quitButtonVictory.onClick.AddListener(() => { PlayMenuSound(); QuitGame(); });

        // Charger les ennemis dans la liste
        UpdateEnemyList();
    }

    void Update()
    {
        if (gameEnded) return;

        if (Input.GetKeyDown(KeyCode.Escape) || IsVRPauseButtonPressed())
        {
            PlayMenuSound();
            TogglePause();
        }

        if (!isPaused)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame(false); // Défaite par manque de temps
            }
        }

        // Vérifier si tous les ennemis sont morts
        CheckVictoryCondition();
    }

    void UpdateTimerUI()
    {
        timeProgressBar.value = timeRemaining;
        float percentage = (timeRemaining / gameTime) * 100f;
        progressText.text = Mathf.CeilToInt(percentage) + "%";
    }

    public void EndGame(bool victory)
    {
        gameEnded = true;
        Time.timeScale = 0f; // Arrêter le temps

        if (victory)
        {
            victoryPanel?.SetActive(true); // Afficher l’écran de victoire
        }
        else
        {
            defeatPanel?.SetActive(true); // Afficher l’écran de défaite
            PauseAllAudioSources(); // Mettre en pause tous les sons
            PlayAlloSelem(); // 🔊 Jouer le son "Allo Selem"
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f; // Remettre le temps normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlayMenuSound()
    {
        if (menuAudioSource != null && menuAudioSource.clip != null)
        {
            menuAudioSource.Play();
        }
    }

    private void PlayAlloSelem()
    {
        if (alloSelemAudio != null)
        {
            alloSelemAudio.Play();
        }
    }

    private void PauseAllAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != alloSelemAudio) // Ne pas stopper "Allo Selem"
            {
                audio.Pause();
            }
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuCanvas?.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ShowHelp()
    {
        helpPanel?.SetActive(true);
        if (helpText != null)
            helpText.gameObject.SetActive(true);
    }

    public void HideHelp()
    {
        helpPanel?.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private bool IsVRPauseButtonPressed()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, devices);
        InputDevices.GetDevicesWithRole(InputDeviceRole.RightHanded, devices);

        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out bool isPressed) && isPressed)
            {
                return true;
            }
        }

        return Input.GetKeyDown(KeyCode.F1);
    }

    private void UpdateEnemyList()
    {
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    public void CheckVictoryCondition()
    {
        enemies.RemoveAll(enemy => enemy == null); // Nettoyer la liste
        if (enemies.Count == 0 && !gameEnded)
        {
            EndGame(true); // ✅ Victoire quand tous les ennemis sont morts
        }
    }

    private void OnDestroy()
    {
        timeProgressBar = null;
        progressText = null;
        pauseMenuCanvas = null;
        helpText = null;
        resumeButton = null;
        helpButton = null;
        quitGameButton = null;
        helpPanel = null;
        backButton = null;
        defeatPanel = null;
        victoryPanel = null;
        restartButton = null;
        quitButton = null;
        enemies = null;
    }
}
