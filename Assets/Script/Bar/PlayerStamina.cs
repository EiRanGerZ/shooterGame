using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina = 100f;

    [Header("Stamina Usage")]
    public float sprintDrain = 20f;
    public float staminaRegen = 15f;

    [Header("UI")]
    public Image frontStaminaBar;
    public Image backStaminaBar;

    [Header("Bar Animation")]
    public float chipSpeed = 0.5f;

    private float lerpTimer;

    void Start()
    {
        stamina = maxStamina;

        frontStaminaBar.fillAmount = 1f;
        backStaminaBar.fillAmount = 1f;

        backStaminaBar.color = Color.yellow;
    }

    void Update()
    {
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        HandleStamina();
        UpdateStaminaUI();
    }

    void HandleStamina()
    {
        // =========================
        // SPRINT / STAMINA BERKURANG
        // =========================
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0f)
        {
            stamina -= sprintDrain * Time.deltaTime;
        }

        // =========================
        // REGEN STAMINA
        // =========================
        else
        {
            stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    void UpdateStaminaUI()
    {
        float fillF = frontStaminaBar.fillAmount;
        float fillB = backStaminaBar.fillAmount;

        float staminaFraction = stamina / maxStamina;

        // ==========================================
        // STAMINA BERKURANG
        // ==========================================
        if (fillF > staminaFraction)
        {
            // Front langsung mengikuti stamina
            frontStaminaBar.fillAmount = staminaFraction;

            // Back menjadi kuning
            backStaminaBar.color = Color.yellow;

            // Back mengejar secara perlahan
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = Mathf.Clamp01(percentComplete);

            backStaminaBar.fillAmount = Mathf.Lerp(
                fillB,
                staminaFraction,
                percentComplete
            );

            // Jika sudah sampai, reset timer
            if (backStaminaBar.fillAmount <= staminaFraction + 0.001f)
            {
                lerpTimer = 0f;
            }
        }

        // ==========================================
        // STAMINA BERTAMBAH / REGEN
        // ==========================================
        else if (fillF < staminaFraction)
        {
            // Back langsung mengikuti stamina
            backStaminaBar.fillAmount = staminaFraction;

            // Back menjadi hijau
            backStaminaBar.color = Color.green;

            // Front mengejar
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = Mathf.Clamp01(percentComplete);

            frontStaminaBar.fillAmount = Mathf.Lerp(
                fillF,
                staminaFraction,
                percentComplete
            );

            // Jika sudah sampai, reset timer
            if (frontStaminaBar.fillAmount >= staminaFraction - 0.001f)
            {
                lerpTimer = 0f;
            }
        }

        // ==========================================
        // STAMINA TIDAK BERUBAH
        // ==========================================
        else
        {
            lerpTimer = 0f;
        }
    }
}