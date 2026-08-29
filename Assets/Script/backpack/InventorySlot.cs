using System;

[Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;

    public bool IsEmpty
    {
        get
        {
            return item == null || amount <= 0;
        }
    }

    public bool CanStack(ItemData newItem)
    {
        return item == newItem && amount < item.maxStack;
    }

    public void Clear()
    {
        item = null;
        amount = 0;
    }
}