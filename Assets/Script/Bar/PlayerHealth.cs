using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float health;

    [Header("Health Bar UI")]
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Health Count UI")]
    public TextMeshProUGUI healthCountText;

    [Header("Animation")]
    public float chipSpeed = 1f;

    private float lerpTimer;

    void Start()
    {
        health = maxHealth;

        if (frontHealthBar != null)
            frontHealthBar.fillAmount = 1f;

        if (backHealthBar != null)
        {
            backHealthBar.fillAmount = 1f;
            backHealthBar.color = Color.green;
        }

        UpdateHealthCount();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);

        UpdateHealthUI();
        UpdateHealthCount();

        // TEST DAMAGE
        // Tekan P untuk testing damage
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(Random.Range(5f, 20f));
        }

        // Tombol H untuk heal sudah dihapus
    }

    void UpdateHealthUI()
    {
        if (frontHealthBar == null || backHealthBar == null)
            return;

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;

        float healthFraction = health / maxHealth;

        // =====================================
        // DAMAGE
        // =====================================
        if (fillF > healthFraction)
        {
            // Front langsung turun
            frontHealthBar.fillAmount = healthFraction;

            // Back merah
            backHealthBar.color = Color.red;

            // Back mengejar perlahan
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = Mathf.Clamp01(percentComplete);

            backHealthBar.fillAmount = Mathf.Lerp(
                fillB,
                healthFraction,
                percentComplete
            );

            if (backHealthBar.fillAmount <= healthFraction + 0.001f)
            {
                backHealthBar.fillAmount = healthFraction;
                lerpTimer = 0f;
            }
        }

        // =====================================
        // HEAL
        // =====================================
        else if (fillF < healthFraction)
        {
            // Back langsung naik
            backHealthBar.fillAmount = healthFraction;

            // Back hijau
            backHealthBar.color = Color.green;

            // Front mengejar perlahan
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = Mathf.Clamp01(percentComplete);

            frontHealthBar.fillAmount = Mathf.Lerp(
                fillF,
                healthFraction,
                percentComplete
            );

            if (frontHealthBar.fillAmount >= healthFraction - 0.001f)
            {
                frontHealthBar.fillAmount = healthFraction;
                lerpTimer = 0f;
            }
        }

        // =====================================
        // TIDAK ADA PERUBAHAN
        // =====================================
        else
        {
            lerpTimer = 0f;
        }
    }

    void UpdateHealthCount()
    {
        if (healthCountText == null)
            return;

        healthCountText.text =
            Mathf.CeilToInt(health) + " / " +
            Mathf.CeilToInt(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0f, maxHealth);

        lerpTimer = 0f;

        UpdateHealthCount();
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0f, maxHealth);

        lerpTimer = 0f;

        UpdateHealthCount();
    }

    // =====================================
    // MEDKIT
    // Heal 50% dari Max Health
    // =====================================
    public bool UseMedkit()
    {
        // Jangan buang Medkit kalau HP sudah penuh
        if (health >= maxHealth)
        {
            Debug.Log("Health sudah penuh. Medkit tidak digunakan.");
            return false;
        }

        float healAmount = maxHealth * 0.5f;

        Heal(healAmount);

        Debug.Log("Medkit digunakan. Heal +" + healAmount);

        return true;
    }
}