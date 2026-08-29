using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverObject;

    private void Start()
    {
        if (hoverObject != null)
        {
            hoverObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverObject != null)
        {
            hoverObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverObject != null)
        {
            hoverObject.SetActive(false);
        }
    }
}