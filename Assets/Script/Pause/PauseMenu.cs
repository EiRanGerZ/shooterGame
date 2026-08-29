using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Menu UI")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    // Properti static agar bisa diakses oleh skrip lain (seperti Aim.cs)
    public static bool IsPaused { get; private set; }

    private void Start()
    {
        // Pastikan menu tidak muncul saat game dimulai
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        IsPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Kalau sedang di Options, kembali ke Pause
            if (optionsMenu.activeSelf)
            {
                BackToPause();
            }
            // Kalau sedang Pause, lanjutkan game
            else if (IsPaused)
            {
                ResumeGame();
            }
            // Kalau sedang gameplay, buka Pause
            else
            {
                PauseGame();
            }
        }
    }

    // =========================
    // PAUSE
    // =========================

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        IsPaused = true;
    }

    // =========================
    // RESUME
    // =========================

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        IsPaused = false;
    }

    // =========================
    // OPTIONS
    // =========================

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // =========================
    // BACK FROM OPTIONS
    // =========================

    public void BackToPause()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    // =========================
    // MAIN MENU
    // =========================

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}