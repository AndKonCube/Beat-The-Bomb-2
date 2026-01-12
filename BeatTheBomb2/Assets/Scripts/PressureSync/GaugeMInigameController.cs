using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls the Gauge Minigame logic, handling input snapping, needle rotation, 
/// random target generation, and the countdown timer.
/// </summary>
public class GaugeMinigameController : MonoBehaviour
{
    [Header("--- Settings ---")]
    public float timeBeforeSwitch = 5.0f; 
    public float minNeedleAngle = 90f;   
    public float maxNeedleAngle = -90f;  

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

    private float currentInput1;
    private float currentInput2;
    private float currentTarget1;
    private float currentTarget2;
    private float timer;

    /// <summary>
    /// Initializes slider ranges, listeners, and starts the first game loop.
    /// </summary>
    private void Start()
    {
        inputSlider1.minValue = 0;
        inputSlider1.maxValue = 3000;
        inputSlider1.onValueChanged.AddListener(OnSlider1Changed);

        inputSlider2.minValue = 0;
        inputSlider2.maxValue = 300;
        inputSlider2.onValueChanged.AddListener(OnSlider2Changed);

        timerSlider.maxValue = timeBeforeSwitch;
        timerSlider.minValue = 0;

        GenerateNewTargets();
    }

    /// <summary>
    /// Handles the countdown timer for the target switch.
    /// </summary>
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

    /// <summary>
    /// Snaps the main slider to increments of 100, updates the text, and rotates the needle.
    /// </summary>
    void OnSlider1Changed(float value)
    {
        float snappedValue = Mathf.Round(value / 100f) * 100f;

        if (value != snappedValue)
        {
            inputSlider1.SetValueWithoutNotify(snappedValue);
        }

        currentInput1 = snappedValue;
        if(valText1 != null) valText1.text = currentInput1.ToString("0");
        
        RotateNeedle(needle1, currentInput1 / 3000f);
    }

    /// <summary>
    /// Snaps the secondary slider to increments of 10, updates the text, and rotates the needle.
    /// </summary>
    void OnSlider2Changed(float value)
    {
        float snappedValue = Mathf.Round(value / 10f) * 10f;

        if (value != snappedValue)
        {
            inputSlider2.SetValueWithoutNotify(snappedValue);
        }

        currentInput2 = snappedValue;
        if (valText2 != null) valText2.text = currentInput2.ToString("0");

        RotateNeedle(needle2, currentInput2 / 300f);
    }

    /// <summary>
    /// Calculates the correct angle for a needle based on the current percentage (fraction) and applies the rotation.
    /// </summary>
    void RotateNeedle(Transform needleTransform, float fraction)
    {
        if (needleTransform == null) return;
        float targetAngle = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, fraction);
        needleTransform.localRotation = Quaternion.Euler(0, 0, targetAngle);
    }

    // --- GAME LOGIC ---

    /// <summary>
    /// Randomizes new target values for the bottom screen and resets the timer.
    /// </summary>
    void GenerateNewTargets()
    {
        timer = timeBeforeSwitch;

        currentTarget1 = Random.Range(0, 31) * 100;
        if(targetText1 != null) targetText1.text = currentTarget1.ToString("0");
        RotateNeedle(targetNeedle1, currentTarget1 / 3000f);

        currentTarget2 = Random.Range(0, 31) * 10;
        if(targetText2 != null) targetText2.text = currentTarget2.ToString("0");
        RotateNeedle(targetNeedle2, currentTarget2 / 300f);

        if(resultText != null) resultText.text = "";
    }

    /// <summary>
    /// Checks if the player's inputs match the current targets when the button is pressed.
    /// </summary>
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