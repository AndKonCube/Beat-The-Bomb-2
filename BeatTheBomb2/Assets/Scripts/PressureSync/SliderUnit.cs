using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderUnit : MonoBehaviour
{
    [Header("UI References")]
    public Slider mySlider;
    public TMP_Text valueText;

    [Header("Settings")]
    public bool isWholeNumber = true; // Set TRUE for PSI, TRUE for Bar
    public float maxValue = 100f;

    // This is the number the Manager reads
    public float CurrentValue { get; private set; }

    void Start()
    {
        // Setup Slider
        if (mySlider != null)
        {
            mySlider.minValue = 0;
            mySlider.maxValue = maxValue;
            mySlider.wholeNumbers = isWholeNumber;
            mySlider.onValueChanged.AddListener(OnSlide);
        }
    }

    public void OnSlide(float val)
    {
        CurrentValue = val;

        // Update Text Display
        if (valueText != null)
        {
            // If whole number, show "1500". If decimal, show "5.2"
            valueText.text = isWholeNumber ? val.ToString("F0") : val.ToString("F1");
        }
    }
}