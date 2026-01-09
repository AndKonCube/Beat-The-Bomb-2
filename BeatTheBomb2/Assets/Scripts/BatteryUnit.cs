using UnityEngine;
using TMPro; // Needed for Text Mesh Pro
using UnityEngine.UI;

public class BatteryUnit : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text numberDisplay;  // Drag the text showing "0" here
    public Button plusButton;       // Drag the "+" button here
    public Button minusButton;      // Drag the "-" button here

    [Header("Data")]
    public int currentValue = 0;    // The current number on this battery

    private OutputManager manager;  // Reference to the boss script

    void Start()
    {
        // Find the manager automatically
        manager = FindFirstObjectByType<OutputManager>();

        // update the text immediately
        UpdateDisplay();

        // Add click listeners to the buttons via code (or you can do it in Inspector)
        plusButton.onClick.AddListener(() => ChangeValue(1));
        minusButton.onClick.AddListener(() => ChangeValue(-1));
    }

    public void ChangeValue(int amount)
    {
        currentValue += amount;

        // Optional: Prevent negative numbers
        if (currentValue < 0) currentValue = 0;

        UpdateDisplay();
        
        // Tell the manager to calculate the total sum every time we change a number
        manager.CheckTotalOutput(); 
    }

    void UpdateDisplay()
    {
        if (numberDisplay != null)
            numberDisplay.text = currentValue.ToString();
    }
}