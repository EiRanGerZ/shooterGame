using UnityEngine;
using Invector.vCharacterController;

public class PlayerSprintController : MonoBehaviour
{
    private vThirdPersonInput thirdPersonInput;
    private vThirdPersonController thirdPersonController;
    private PlayerStamina playerStamina;

    void Start()
    {
        // Ambil komponen Invector dari Player
        thirdPersonInput = GetComponent<vThirdPersonInput>();
        thirdPersonController = GetComponent<vThirdPersonController>();

        // Ambil script stamina
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        if (thirdPersonInput == null)
        {
            Debug.LogError("vThirdPersonInput tidak ditemukan!");
            return;
        }

        if (thirdPersonController == null)
        {
            Debug.LogError("vThirdPersonController tidak ditemukan!");
            return;
        }

        if (playerStamina == null)
        {
            Debug.LogError("PlayerStamina tidak ditemukan!");
            return;
        }

        // =========================================
        // STAMINA HABIS
        // =========================================

        if (playerStamina.stamina <= 0f)
        {
            // Paksa Invector berhenti sprint
            thirdPersonController.Sprint(false);
        }
    }
}