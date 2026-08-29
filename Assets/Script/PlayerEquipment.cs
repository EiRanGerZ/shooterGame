using UnityEngine;
using TMPro;

public class WeaponUISelector : MonoBehaviour
{
    [Header("Weapon UI")]
    public GameObject mainWeapon;
    public GameObject meleeWeapon;

    [Header("Shortcut Number")]
    public TextMeshProUGUI key1;
    public TextMeshProUGUI key2;

    private void Start()
    {
        SelectMainWeapon();
    }

    private void Update()
    {
        if (PauseMenu.IsPaused)
             return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectMainWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectMeleeWeapon();
        }
    }

    void SelectMainWeapon()
    {
        mainWeapon.SetActive(true);
        meleeWeapon.SetActive(false);

        SetOpacity(key1, 1f);
        SetOpacity(key2, 0.5f);
    }

    void SelectMeleeWeapon()
    {
        mainWeapon.SetActive(false);
        meleeWeapon.SetActive(true);

        SetOpacity(key1, 0.5f);
        SetOpacity(key2, 1f);
    }

    void SetOpacity(TextMeshProUGUI text, float opacity)
    {
        Color color = text.color;
        color.a = opacity;
        text.color = color;
    }
}