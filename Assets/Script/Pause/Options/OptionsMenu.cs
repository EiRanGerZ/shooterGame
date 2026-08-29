using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject optionsPanel;
    public GameObject pausePanel;

    [Header("Master Volume")]
    public Slider volumeSlider;

    [Header("Sensitivity")]
    public Slider sensitivitySlider;

    [Header("Fullscreen")]
    public GameObject fullscreenOn;
    public GameObject fullscreenOff;

    private bool isFullscreen;

    private void Start()
    {
        // Master Volume
        volumeSlider.value = AudioListener.volume;

        // Ambil kondisi fullscreen saat game mulai
        isFullscreen = Screen.fullScreen;

        UpdateFullscreenUI();
    }

    // =========================
    // MASTER VOLUME
    // =========================

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    // =========================
    // SENSITIVITY
    // =========================

    public void SetSensitivity(float value)
    {
        Debug.Log("Sensitivity: " + value);

        // Nanti disambungkan ke Invector Camera
    }

    // =========================
    // FULLSCREEN
    // =========================

    public void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;

        Screen.fullScreen = isFullscreen;

        UpdateFullscreenUI();
    }

    private void UpdateFullscreenUI()
    {
        fullscreenOn.SetActive(isFullscreen);
        fullscreenOff.SetActive(!isFullscreen);
    }

    // =========================
    // BACK
    // =========================

    public void Back()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}