using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    [Header("--- Player 1 (The Input) ---")]
    public SliderUnit sliderPSI; // Drag Left Slider (Input)
    public SliderUnit sliderBar; // Drag Right Slider (Input)
    
    [Header("--- Player 1 Visuals (Rotating Ticks) ---")]
    public RectTransform needlePSI; // Drag the Image/Needle for PSI
    public RectTransform needleBar; // Drag the Image/Needle for Bar
    public float minAngle = 90f;    // Rotation at 0 (e.g. Left)
    public float maxAngle = -90f;   // Rotation at Max (e.g. Right)

    [Header("--- Player 2 (The Map) ---")]
    public GaugeTarget gaugePSI; // Drag Left Gauge (Target Display)
    public GaugeTarget gaugeBar; // Drag Right Gauge (Target Display)
    public Slider timerSlider;   // Drag the Timer Slider here

    [Header("--- Settings ---")]
    public float maxPSI = 3000f;
    public float maxBar = 10f;
    public float timePerRound = 10f; // How many seconds before switch

    [Header("--- Feedback ---")]
    public TMP_Text statusText; // "SYSTEM ONLINE" text

    private float currentTimer;
    private bool isGameActive = true;

    void Start()
    {
        // Initialize Player 1 Inputs
        if(sliderPSI) { sliderPSI.maxValue = maxPSI; sliderPSI.isWholeNumber = true; }
        if(sliderBar) { sliderBar.maxValue = maxBar; sliderBar.isWholeNumber = true; }

        // Initialize Timer
        if(timerSlider) 
        {
            timerSlider.maxValue = timePerRound;
            timerSlider.interactable = false; // Prevent player form moving it
        }

        // Start the first round
        GenerateRandomNumbers();
    }

    void Update()
    {
        if (!isGameActive) return;

        // --- 1. HANDLE TIMER ---
        currentTimer -= Time.deltaTime;
        
        // Update the visual timer bar
        if(timerSlider) timerSlider.value = currentTimer;

        // If time runs out, switch numbers!
        if (currentTimer <= 0)
        {
            GenerateRandomNumbers();
        }

        // --- 2. HANDLE VISUAL ROTATION (PLAYER 1) ---
        // Rotate needles based on the current slider values
        if(sliderPSI && needlePSI) RotateNeedle(needlePSI, sliderPSI.CurrentValue, maxPSI);
        if(sliderBar && needleBar) RotateNeedle(needleBar, sliderBar.CurrentValue, maxBar);
    }

    void GenerateRandomNumbers()
    {
        // Reset Timer
        currentTimer = timePerRound;

        // 1. Generate Random Answer Keys
        float randomPSI = Mathf.RoundToInt(Random.Range(0, maxPSI));
        float randomBar = Mathf.RoundToInt(Random.Range(0, maxBar));

        // 2. Configure Player 2's Screens (The Targets)
        if(gaugePSI) gaugePSI.SetupGauge(randomPSI, maxPSI);
        if(gaugeBar) gaugeBar.SetupGauge(randomBar, maxBar);

        // 3. Reset Feedback Text
        if(statusText) 
        {
            statusText.text = "PRESSURE UNSTABLE";
            statusText.color = Color.white;
        }
    }

    // Helper function to rotate the UI images
    void RotateNeedle(RectTransform needle, float currentVal, float maxVal)
    {
        float percent = Mathf.Clamp01(currentVal / maxVal);
        float angle = Mathf.Lerp(minAngle, maxAngle, percent);
        needle.localRotation = Quaternion.Euler(0, 0, angle);
    }

    // Connect this to a "SUBMIT" button
    public void CheckSystem()
    {
        bool psiCorrect = false;
        bool barCorrect = false;

        // --- CHECK PSI ---
        if (Mathf.Approximately(sliderPSI.CurrentValue, gaugePSI.targetValue))
        {
            psiCorrect = true;
        }

        // --- CHECK BAR ---
        if (Mathf.Approximately(sliderBar.CurrentValue, gaugeBar.targetValue))
        {
            barCorrect = true;
        }

        // --- RESULT ---
        if (psiCorrect && barCorrect)
        {
            Debug.Log("WIN!");
            if(statusText)
            {
                statusText.text = "PRESSURE STABILIZED";
                statusText.color = Color.green;
            }
            // Optional: Pause game on win?
            // isGameActive = false; 
        }
        else
        {
            Debug.Log($"FAIL: PSI {sliderPSI.CurrentValue}/{gaugePSI.targetValue} | BAR {sliderBar.CurrentValue}/{gaugeBar.targetValue}");
            if(statusText)
            {
                statusText.text = "ERROR: MISMATCH";
                statusText.color = Color.red;
            }
        }
    }
}