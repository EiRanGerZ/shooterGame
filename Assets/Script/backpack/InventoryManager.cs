using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private GameObject inventoryUI;

    [Header("HUD")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private PlayerUI playerUI;

    [Header("Player Interaction")]
    [SerializeField] private PlayerInteract playerInteract;

    [Header("Input")]
    [SerializeField] private InputActionReference backpackAction;

    private bool isInventoryOpen = false;

    private void Start()
    {
        if (inventoryUI != null)
            inventoryUI.SetActive(false);

        if (crosshair != null)
            crosshair.SetActive(true);

        if (playerUI != null)
            playerUI.HideLootUI();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        if (backpackAction != null)
            backpackAction.action.Enable();
    }

    private void OnDisable()
    {
        if (backpackAction != null)
            backpackAction.action.Disable();
    }

    private void Update()
    {
        if (PauseMenu.IsPaused)
            return;

        if (backpackAction != null &&
            backpackAction.action.WasPressedThisFrame())
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (inventoryUI != null)
            inventoryUI.SetActive(isInventoryOpen);

        if (crosshair != null)
            crosshair.SetActive(!isInventoryOpen);

        if (playerUI != null && isInventoryOpen)
            playerUI.HideLootUI();

        if (playerInteract != null)
            playerInteract.enabled = !isInventoryOpen;

        if (isInventoryOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}