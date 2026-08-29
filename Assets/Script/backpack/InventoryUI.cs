using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;

    [Header("UI Slots")]
    [SerializeField] private InventorySlotUI[] slotUIs;

    private void Start()
    {
        RefreshUI();
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (inventory == null)
        {
            Debug.LogWarning("InventoryUI: Inventory belum di-assign!");
            return;
        }

        if (slotUIs == null || slotUIs.Length == 0)
        {
            Debug.LogWarning("InventoryUI: Slot UI belum di-assign!");
            return;
        }

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (slotUIs[i] == null)
                continue;

            if (i < inventory.slots.Length)
            {
                slotUIs[i].UpdateSlot(inventory.slots[i]);
            }
            else
            {
                slotUIs[i].ClearSlot();
            }
        }
    }
}