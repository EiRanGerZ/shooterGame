using UnityEngine;

public enum ItemType
{
    Fuel,
    Medkit,
    Screw
}

[CreateAssetMenu(
    fileName = "New Item",
    menuName = "Inventory/Item"
)]
public class ItemData : ScriptableObject
{
    [Header("Item")]
    public string itemName;

    [Header("Item Type")]
    public ItemType itemType;

    [Header("Inventory Icon")]
    public Sprite icon;

    [Header("Stack")]
    [Min(1)]
    public int maxStack = 1;
}