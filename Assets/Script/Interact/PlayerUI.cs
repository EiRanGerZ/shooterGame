using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Loot UI")]
    [SerializeField] private GameObject lootUI;

    [Header("Loot Type UI")]
    [SerializeField] private GameObject fuelUI;
    [SerializeField] private GameObject medkitUI;
    [SerializeField] private GameObject screwUI;

    public void ShowLootUI(ItemType itemType)
    {
        if (lootUI != null)
        {
            lootUI.SetActive(true);
        }

        HideAllItemUI();

        switch (itemType)
        {
            case ItemType.Fuel:
                if (fuelUI != null)
                {
                    fuelUI.SetActive(true);
                }
                break;

            case ItemType.Medkit:
                if (medkitUI != null)
                {
                    medkitUI.SetActive(true);
                }
                break;

            case ItemType.Screw:
                if (screwUI != null)
                {
                    screwUI.SetActive(true);
                }
                break;
        }
    }

    public void HideLootUI()
    {
        if (lootUI != null)
        {
            lootUI.SetActive(false);
        }

        HideAllItemUI();
    }

    private void HideAllItemUI()
    {
        if (fuelUI != null)
        {
            fuelUI.SetActive(false);
        }

        if (medkitUI != null)
        {
            medkitUI.SetActive(false);
        }

        if (screwUI != null)
        {
            screwUI.SetActive(false);
        }
    }
}