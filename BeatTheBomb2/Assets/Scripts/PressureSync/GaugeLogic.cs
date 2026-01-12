using UnityEngine;
using UnityEngine.UI;

public class GaugeLogic : MonoBehaviour
{
    [Header("Gauge Settings")]
    public float maxValue = 100f; // Set to 1000 for PSI, 100 for Bar
    public float stepSize = 10f;  // Set to 100 for PSI, 10 for Bar

    [Header("UI References")]
    public Transform needle;      // Drag needle here (if PSI)
    public Image barFill;         // Drag fill image here (if Bar)
    public Slider controlSlider;  // Drag the slider for Screen 1 here

    private float currentValue;

    void Start()
    {
        // If there is a slider connected, set its max value automatically
        if (controlSlider != null)
        {
            controlSlider.minValue = 0;
            controlSlider.maxValue = maxValue;
            
            // IMPORTANT: Make the slider integer-based if you want it to feel "snappy"
            controlSlider.wholeNumbers = false; // We handle the snapping in code below
        }
    }

    // ---------------------------------------------------------
    // SCREEN 1: CONTROLLED BY SLIDER
    // Link this to the Slider's "On Value Changed" event
    // ---------------------------------------------------------
    public void OnSliderDrag(float val)
    {
        // Snap the slider value to the nearest Step Size
        float snappedValue = SnapToStep(val);
        
        UpdateVisuals(snappedValue);
    }

    // ---------------------------------------------------------
    // SCREEN 2: CONTROLLED RANDOMLY
    // Call this when Screen 2 opens
    // ---------------------------------------------------------
    public void RandomizeValue()
    {
        // 1. Pick a random number in range
        float randomRaw = Random.Range(0f, maxValue);

        // 2. Force it to snap to the exact same steps as the slider (10, 20, etc.)
        float snappedValue = SnapToStep(randomRaw);

        UpdateVisuals(snappedValue);
    }

    // ---------------------------------------------------------
    // SHARED MATH & VISUALS
    // ---------------------------------------------------------
    
    // This is the magic formula that forces 100, 200... or 10, 20...
    private float SnapToStep(float rawInput)
    {
        if (stepSize <= 0) return rawInput; // Safety check
        return Mathf.Round(rawInput / stepSize) * stepSize;
    }

    private void UpdateVisuals(float val)
    {
        currentValue = val;

        // Calculate percentage (0.0 to 1.0)
        float percent = Mathf.Clamp01(currentValue / maxValue);

        // 1. Handle Needle Rotation (PSI)
        if (needle != null)
        {
            // Assumes gauge goes from -90 (empty) to 90 (full)
            float angle = Mathf.Lerp(-90f, 90f, percent);
            needle.localRotation = Quaternion.Euler(0, 0, angle);
        }

        // 2. Handle Bar Fill (Bar Gauge)
        if (barFill != null)
        {
            barFill.fillAmount = percent;
        }
    }
}