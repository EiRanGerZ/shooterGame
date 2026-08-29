using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public TMP_InputField dayText;

    private int currentDay = 1;

    private void Start()
    {
        UpdateDayText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            currentDay++;
            UpdateDayText();
        }
    }

    private void UpdateDayText()
    {
        dayText.text = "DAY " + currentDay;
    }
}