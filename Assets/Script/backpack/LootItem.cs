using UnityEngine;

public class LootItem : Interactible
{
    [Header("Item")]
    [SerializeField] private ItemData item;

    [Header("Amount")]
    [SerializeField] private int amount = 1;

    public ItemData Item => item;

    protected override void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning("Inventory tidak ditemukan!");
            return;
        }

        if (item == null)
        {
            Debug.LogWarning("ItemData pada LootItem belum diisi!");
            return;
        }

        int remainingAmount = inventory.AddItem(item, amount);

        Debug.Log(
            "Loot: " + item.itemName +
            " | Amount: " + amount +
            " | Remaining: " + remainingAmount
        );

        if (remainingAmount <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            amount = remainingAmount;
        }
    }
}