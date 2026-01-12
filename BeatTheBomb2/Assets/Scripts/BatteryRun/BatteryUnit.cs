using UnityEngine;
using TMPro; 
using UnityEngine.UI;

/// <summary>
/// Manages an individual battery unit, allowing players to adjust 
/// amperage and voltage values via UI buttons.
/// </summary>
public class BatteryUnit : MonoBehaviour
{
    [Header("--- LEFT SIDE (Amperemeter) ---")]
    public TMP_Text ampText;      
    public Button ampPlusBtn;      
    public Button ampMinusBtn;     
    public int currentAmps = 0;    

    [Header("--- RIGHT SIDE (Voltmeter) ---")]
    public TMP_Text voltText;      
    public Button voltPlusBtn;     
    public Button voltMinusBtn;    
    public int currentVolts = 0;   

    /// <summary>
    /// Initializes UI displays and assigns button click listeners.
    /// </summary>
    void Start()
    {
        UpdateDisplays();

        if(ampPlusBtn) ampPlusBtn.onClick.AddListener(() => ChangeAmps(1));
        if(ampMinusBtn) ampMinusBtn.onClick.AddListener(() => ChangeAmps(-1));
        if(voltPlusBtn) voltPlusBtn.onClick.AddListener(() => ChangeVolts(1));
        if(voltMinusBtn) voltMinusBtn.onClick.AddListener(() => ChangeVolts(-1));   
    }

    /// <summary>
    /// Adjusts the current amperage by the specified amount and updates the display.
    /// Values cannot go below zero.
    /// </summary>
    /// <param name="amount">The amount to add or subtract.</param>
    public void ChangeAmps(int amount)
    {
        currentAmps += amount;
        if (currentAmps < 0) currentAmps = 0;
        
        if (ampText != null) ampText.text = currentAmps.ToString();
    }

    /// <summary>
    /// Adjusts the current voltage by the specified amount and updates the display.
    /// Values cannot go below zero.
    /// </summary>
    /// <param name="amount">The amount to add or subtract.</param>
    public void ChangeVolts(int amount)
    {
        currentVolts += amount;
        if (currentVolts < 0) currentVolts = 0;

        if (voltText != null) voltText.text = currentVolts.ToString();
    }

    /// <summary>
    /// Refreshes the text elements to match the current internal values.
    /// </summary>
    void UpdateDisplays()
    {
        if (ampText != null) ampText.text = currentAmps.ToString();
        if (voltText != null) voltText.text = currentVolts.ToString();
    }
}