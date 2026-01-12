using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeTarget : MonoBehaviour
{
    [Header("Visuals")]
    public Image fillImage;   // The Radial Fill Image (The "Needle" or Bar)
    public TMP_Text targetText; // Shows the target number explicitly

    [Header("Data")]
    public float targetValue; // The Manager sets this randomly
    public float maxValue;    // Used to calculate fill percentage

    public void SetupGauge(float newTarget, float max)
    {
        targetValue = newTarget;
        maxValue = max;

        // 1. Update the visual fill (0.0 to 1.0)
        if (fillImage != null && maxValue > 0)
        {
            fillImage.fillAmount = targetValue / maxValue;
        }

        // 2. Show the text so Player 2 can read it out loud
        if (targetText != null)
        {
            targetText.text = targetValue.ToString("F0");
        }
    }
}