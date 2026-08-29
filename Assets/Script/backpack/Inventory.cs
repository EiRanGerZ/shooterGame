using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int inventorySize = 15;

    public InventorySlot[] slots;

    private void Awake()
    {
        slots = new InventorySlot[inventorySize];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public int AddItem(ItemData item, int amount)
    {
        if (item == null || amount <= 0)
            return amount;

        int remainingAmount = amount;

        // =====================================
        // ISI STACK YANG SUDAH ADA
        // =====================================
        for (int i = 0; i < slots.Length; i++)
        {
            if (remainingAmount <= 0)
                break;

            InventorySlot slot = slots[i];

            if (slot.CanStack(item))
            {
                int availableSpace = item.maxStack - slot.amount;

                int amountToAdd = Mathf.Min(
                    availableSpace,
                    remainingAmount
                );

                slot.amount += amountToAdd;
                remainingAmount -= amountToAdd;

                Debug.Log(
                    "Stack inventory slot " + i +
                    ": " + item.itemName +
                    " x" + slot.amount
                );
            }
        }

        // =====================================
        // CARI SLOT KOSONG
        // =====================================
        for (int i = 0; i < slots.Length; i++)
        {
            if (remainingAmount <= 0)
                break;

            InventorySlot slot = slots[i];

            if (slot.IsEmpty)
            {
                int amountToAdd = Mathf.Min(
                    item.maxStack,
                    remainingAmount
                );

                slot.item = item;
                slot.amount = amountToAdd;

                remainingAmount -= amountToAdd;

                Debug.Log(
                    "Masuk inventory slot " + i +
                    ": " + item.itemName +
                    " x" + amountToAdd
                );
            }
        }

        return remainingAmount;
    }

    // =====================================
    // HAPUS 1 ITEM DARI SLOT
    // =====================================
    public void RemoveOneItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
            return;

        InventorySlot slot = slots[slotIndex];

        if (slot == null || slot.IsEmpty)
            return;

        slot.amount--;

        // Kalau jumlah sudah 0, kosongkan slot
        if (slot.amount <= 0)
        {
            slot.Clear();
        }
    }
}