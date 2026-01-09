using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CollaborationManager : MonoBehaviour
{
    [Header("Display 2 Components")]
    public List<SlotLogic> slots;      
    public List<MapToken> tokens;      

    [Header("UI Feedback")]
    public TMP_Text statusText; 

    void Start()
    {
        if(statusText) statusText.text = "SYSTEM OFFLINE";
        ShuffleTokens();
    }

    void ShuffleTokens()
    {
        List<MapToken> shuffled = new List<MapToken>(tokens);
        
        // Shuffle
        for (int i = 0; i < shuffled.Count; i++)
        {
            MapToken temp = shuffled[i];
            int rand = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[rand];
            shuffled[rand] = temp;
        }

        // Place in slots
        for (int i = 0; i < 4; i++)
        {
            if (i < slots.Count && i < shuffled.Count)
            {
                shuffled[i].transform.SetParent(slots[i].transform, false);
                shuffled[i].transform.localPosition = Vector3.zero;
            }
        }
    }

    public void CheckPuzzle()
    {
        Debug.Log("--- CHECKING PUZZLE ---"); // 1. Confirm function runs
        bool allCorrect = true;

        foreach (SlotLogic slot in slots)
        {
            MapToken tokenInSlot = slot.GetCurrentToken();

            if (tokenInSlot != null)
            {
                BatteryUnit realBattery = tokenInSlot.realBattery;

                // Safety Check: Is the link missing?
                if (realBattery == null)
                {
                    Debug.LogError($"ERROR: Token {tokenInSlot.name} is not linked to a Real Battery!");
                    return;
                }

                // PRINT THE COMPARISON
                Debug.Log($"Slot '{slot.name}' wants Amps:{slot.correctAmps} / Volts:{slot.correctVolts}. " +
                          $"Battery '{realBattery.name}' has Amps:{realBattery.currentAmps} / Volts:{realBattery.currentVolts}");

                // Check Amps
                if (realBattery.currentAmps != slot.correctAmps)
                {
                    allCorrect = false;
                    Debug.Log("-> AMP MISMATCH");
                }

                // Check Volts
                if (realBattery.currentVolts != slot.correctVolts)
                {
                    allCorrect = false;
                    Debug.Log("-> VOLT MISMATCH");
                }
            }
            else
            {
                Debug.LogWarning($"Slot {slot.name} is empty!");
            }
        }

        if (allCorrect)
        {
            Debug.Log("WIN CONDITION MET!");
            if(statusText) 
            {
                statusText.text = "SYSTEM ONLINE";
                statusText.color = Color.green;
            }
        }
    }
}