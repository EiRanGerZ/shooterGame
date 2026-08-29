using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject useText;

    [Header("Slot")]
    [SerializeField] private int slotIndex;

    private Inventory inventory;
    private PlayerHealth playerHealth;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        inventory = FindFirstObjectByType<Inventory>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();

        if (button != null)
        {
            button.onClick.AddListener(OnSlotClicked);
        }
    }

    public void UpdateSlot(InventorySlot slot)
    {
        // =========================
        // SLOT KOSONG
        // =========================
        if (slot == null || slot.IsEmpty)
        {
            ClearSlot();
            return;
        }

        // =========================
        // ICON ITEM
        // =========================
        if (itemIcon != null)
        {
            itemIcon.sprite = slot.item.icon;
            itemIcon.enabled = true;
        }

        // =========================
        // AMOUNT
        // =========================
        if (amountText != null)
        {
            if (slot.amount > 1)
            {
                amountText.gameObject.SetActive(true);
                amountText.text = slot.amount.ToString();
            }
            else
            {
                amountText.gameObject.SetActive(false);
            }
        }

        // =========================
        // CLICK TO USE
        // HANYA MEDKIT
        // =========================
        if (useText != null)
        {
            useText.SetActive(
                slot.item.itemType == ItemType.Medkit
            );
        }

        // =========================
        // BUTTON
        // SEMUA ITEM TETAP TERANG
        // =========================
        if (button != null)
        {
            button.interactable = true;
        }
    }

    public void ClearSlot()
    {
        // =========================
        // HILANGKAN ICON
        // =========================
        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }

        // =========================
        // HILANGKAN AMOUNT
        // =========================
        if (amountText != null)
        {
            amountText.text = "";
            amountText.gameObject.SetActive(false);
        }

        // =========================
        // HILANGKAN USE TEXT
        // =========================
        if (useText != null)
        {
            useText.SetActive(false);
        }

        // Slot kosong tidak bisa diklik
        if (button != null)
        {
            button.interactable = false;
        }
    }

    private void OnSlotClicked()
    {
        if (inventory == null || playerHealth == null)
            return;

        if (slotIndex < 0 || slotIndex >= inventory.slots.Length)
            return;

        InventorySlot slot = inventory.slots[slotIndex];

        if (slot == null || slot.IsEmpty)
            return;

        // =========================
        // HANYA MEDKIT YANG BISA DIPAKAI
        // =========================
        if (slot.item.itemType != ItemType.Medkit)
            return;

        bool used = playerHealth.UseMedkit();

        // =========================
        // MEDKIT BERHASIL DIPAKAI
        // =========================
        if (used)
        {
            inventory.RemoveOneItem(slotIndex);

            // Update tampilan slot
            UpdateSlot(inventory.slots[slotIndex]);
        }
    }
}