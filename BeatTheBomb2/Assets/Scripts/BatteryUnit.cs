using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class BatteryUnit : MonoBehaviour
{
    [Header("--- LEFT SIDE (Amperemeter) ---")]
    public TMP_Text ampText;       // Drag the LEFT text here
    public Button ampPlusBtn;      // Drag LEFT (+)
    public Button ampMinusBtn;     // Drag LEFT (-)
    public int currentAmps = 0;    // Holds the Amperemeter value

    [Header("--- RIGHT SIDE (Voltmeter) ---")]
    public TMP_Text voltText;      // Drag the RIGHT text here
    public Button voltPlusBtn;     // Drag RIGHT (+)
    public Button voltMinusBtn;    // Drag RIGHT (-)
    public int currentVolts = 0;   // Holds the Voltmeter value

    private CollaborationManager manager; 

    void Start()
    {
        manager = FindFirstObjectByType<CollaborationManager>();
        UpdateDisplays();

        // --- Setup Listeners for LEFT (Amps) ---
        if(ampPlusBtn) ampPlusBtn.onClick.AddListener(() => ChangeAmps(1));
        if(ampMinusBtn) ampMinusBtn.onClick.AddListener(() => ChangeAmps(-1));

        // --- Setup Listeners for RIGHT (Volts) ---
        if(voltPlusBtn) voltPlusBtn.onClick.AddListener(() => ChangeVolts(1));
        if(voltMinusBtn) voltMinusBtn.onClick.AddListener(() => ChangeVolts(-1));   
    }

    public void ChangeAmps(int amount)
    {
        currentAmps += amount;
        if (currentAmps < 0) currentAmps = 0;
        
        if (ampText != null) ampText.text = currentAmps.ToString();
        if (manager != null) manager.CheckPuzzle();
    }

    public void ChangeVolts(int amount)
    {
        currentVolts += amount;
        if (currentVolts < 0) currentVolts = 0;

        if (voltText != null) voltText.text = currentVolts.ToString();
        if (manager != null) manager.CheckPuzzle();
    }

    void UpdateDisplays()
    {
        if (ampText != null) ampText.text = currentAmps.ToString();
        if (voltText != null) voltText.text = currentVolts.ToString();
    }
}