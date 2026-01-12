using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeMinigameController : MonoBehaviour
{
    [Header("--- Settings ---")]
    public float timeBeforeSwitch = 5.0f; 
    public float minNeedleAngle = 90f;   // Start rotation (0 value)
    public float maxNeedleAngle = -90f;  // End rotation (Max value)

    [Header("--- Screen 1 (Player Input) ---")]
    public Slider inputSlider1; 
    public Slider inputSlider2; 
    public Transform needle1;   
    public Transform needle2;   
    public TextMeshProUGUI valText1;
    public TextMeshProUGUI valText2;

    [Header("--- Screen 2 (Target) ---")]
    public Slider timerSlider;   
    public Transform targetNeedle1; 
    public Transform targetNeedle2; 
    public TextMeshProUGUI targetText1;
    public TextMeshProUGUI targetText2;

    [Header("--- Feedback ---")]
    public TextMeshProUGUI resultText; 

    // Private variables
    private float currentInput1;
    private float currentInput2;
    private float currentTarget1;
    private float currentTarget2;
    private float timer;

    private void Start()
    {
        // Initialize Sliders
        inputSlider1.minValue = 0;
        inputSlider1.maxValue = 3000;
        // Note: We remove the listener here and add it in the Inspector to be safe, 
        // or you can keep it here. For this fix, let's rely on the code.
        inputSlider1.onValueChanged.AddListener(OnSlider1Changed);

        inputSlider2.minValue = 0;
        inputSlider2.maxValue = 300;
        inputSlider2.onValueChanged.AddListener(OnSlider2Changed);

        // Initialize Timer
        timerSlider.maxValue = timeBeforeSwitch;
        timerSlider.minValue = 0;

        GenerateNewTargets();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timerSlider != null) timerSlider.value = timer;

        if (timer <= 0)
        {
            GenerateNewTargets();
        }
    }

    // --- INPUT LOGIC ---

    void OnSlider1Changed(float value)
    {
        // 1. Calculate Snapped Value (Step 100)
        float snappedValue = Mathf.Round(value / 100f) * 100f;

        // 2. Force Slider visual to snap
        if (value != snappedValue)
        {
            inputSlider1.SetValueWithoutNotify(snappedValue);
        }

        // 3. Update Logic
        currentInput1 = snappedValue;
        if(valText1 != null) valText1.text = currentInput1.ToString("0");
        
        RotateNeedle(needle1, currentInput1 / 3000f);
    }

    void OnSlider2Changed(float value)
    {
        // 1. Calculate Snapped Value (Step 10)
        float snappedValue = Mathf.Round(value / 10f) * 10f;

        // 2. Force Slider visual to snap
        if (value != snappedValue)
        {
            inputSlider2.SetValueWithoutNotify(snappedValue);
        }

        // 3. Update Logic
        currentInput2 = snappedValue;
        if (valText2 != null) valText2.text = currentInput2.ToString("0");

        RotateNeedle(needle2, currentInput2 / 300f);
    }

    void RotateNeedle(Transform needleTransform, float fraction)
    {
        if (needleTransform == null) return;
        float targetAngle = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, fraction);
        needleTransform.localRotation = Quaternion.Euler(0, 0, targetAngle);
    }

    // --- GAME LOGIC ---

    void GenerateNewTargets()
    {
        timer = timeBeforeSwitch;

        // Target 1 (0-3000)
        currentTarget1 = Random.Range(0, 31) * 100;
        if(targetText1 != null) targetText1.text = currentTarget1.ToString("0");
        RotateNeedle(targetNeedle1, currentTarget1 / 3000f);

        // Target 2 (0-300)
        currentTarget2 = Random.Range(0, 31) * 10;
        if(targetText2 != null) targetText2.text = currentTarget2.ToString("0");
        RotateNeedle(targetNeedle2, currentTarget2 / 300f);

        if(resultText != null) resultText.text = "";
    }

    public void OnPushButtonPressed()
    {
        bool match1 = Mathf.Approximately(currentInput1, currentTarget1);
        bool match2 = Mathf.Approximately(currentInput2, currentTarget2);

        if (match1 && match2)
        {
            Debug.Log("WIN");
            if (resultText != null) { resultText.text = "SUCCESS"; resultText.color = Color.green; }
        }
        else
        {
            Debug.Log("LOSE");
            if (resultText != null) { resultText.text = "FAIL"; resultText.color = Color.red; }
        }
    }
}