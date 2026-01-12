using UnityEngine;
using TMPro; 
using UnityEngine.UI;

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

    // We don't even need the manager reference here anymore!

    void Start()
    {
        UpdateDisplays();

        // Listeners
        if(ampPlusBtn) ampPlusBtn.onClick.AddListener(() => ChangeAmps(1));
        if(ampMinusBtn) ampMinusBtn.onClick.AddListener(() => ChangeAmps(-1));
        if(voltPlusBtn) voltPlusBtn.onClick.AddListener(() => ChangeVolts(1));
        if(voltMinusBtn) voltMinusBtn.onClick.AddListener(() => ChangeVolts(-1));   
    }

    public void ChangeAmps(int amount)
    {
        currentAmps += amount;
        if (currentAmps < 0) currentAmps = 0;
        
        if (ampText != null) ampText.text = currentAmps.ToString();
        // REMOVED: manager.CheckPuzzle(); 
    }

    public void ChangeVolts(int amount)
    {
        currentVolts += amount;
        if (currentVolts < 0) currentVolts = 0;

        if (voltText != null) voltText.text = currentVolts.ToString();
        // REMOVED: manager.CheckPuzzle();
    }

    void UpdateDisplays()
    {
        if (ampText != null) ampText.text = currentAmps.ToString();
        if (voltText != null) voltText.text = currentVolts.ToString();
    }
}