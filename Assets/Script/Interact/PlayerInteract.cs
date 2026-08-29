using UnityEngine;
using UnityEngine.InputSystem;
using Invector.vCharacterController;

public class PlayerInteract : MonoBehaviour
{
    [Header("Invector Camera")]
    [SerializeField] private vThirdPersonCamera playerCamera;

    private Camera cam;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private PlayerUI playerUI;

    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;

    private void Start()
    {
        if (playerCamera != null)
        {
            cam = playerCamera.GetComponentInChildren<Camera>();
        }

        if (cam == null)
        {
            cam = Camera.main;
        }

        if (playerUI == null)
        {
            playerUI = GetComponentInChildren<PlayerUI>();
        }

        if (playerUI != null)
        {
            playerUI.HideLootUI();
        }
    }

    private void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.Disable();
        }

        if (playerUI != null)
        {
            playerUI.HideLootUI();
        }
    }

    private void Update()
    {
        if (PauseMenu.IsPaused)
            return;
            
        if (cam == null || playerUI == null)
            return;

        playerUI.HideLootUI();

        Ray ray = new Ray(
            cam.transform.position,
            cam.transform.forward
        );

        Debug.DrawRay(
            ray.origin,
            ray.direction * interactionDistance,
            Color.red
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hitInfo,
            interactionDistance,
            mask
        ))
        {
            LootItem lootItem =
                hitInfo.collider.GetComponentInParent<LootItem>();

            if (lootItem != null && lootItem.Item != null)
            {
                playerUI.ShowLootUI(
                    lootItem.Item.itemType
                );

                if (interactAction != null &&
                    interactAction.action.WasPressedThisFrame())
                {
                    lootItem.BaseInteract();
                }
            }
        }
    }
}