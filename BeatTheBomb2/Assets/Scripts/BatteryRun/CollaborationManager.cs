using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class CollaborationManager : MonoBehaviour
{
    [Header("Display 2 Components (The Map)")]
    public List<SlotLogic> slots;      
    public List<MapToken> tokens;      
    [Header("UI Feedback")]
    public TMP_Text statusText;       

    void Start()
    {
        if (statusText != null)
        {
            statusText.text = "SYSTEM OFFLINE";
            statusText.color = Color.white;
        }

        ShuffleTokens();
    }

    void ShuffleTokens()
    {
        List<MapToken> shuffled = new List<MapToken>(tokens);

        for (int i = 0; i < shuffled.Count; i++)
        {
            MapToken temp = shuffled[i];
            int rand = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[rand];
            shuffled[rand] = temp;
        }

        for (int i = 0; i < 4; i++)
        {

            if (i < slots.Count && i < shuffled.Count)
            {

                shuffled[i].transform.SetParent(slots[i].transform, false);

               
                shuffled[i].transform.localPosition = Vector3.zero;
            }
        }
    }

    public async void CheckPuzzle()
    {
        Debug.Log("Checking System Integrity...");
        bool allCorrect = true;

        foreach (SlotLogic slot in slots)
        {
            MapToken tokenInSlot = slot.GetCurrentToken();

            if (tokenInSlot != null)
            {

                BatteryUnit realBattery = tokenInSlot.realBattery;

                if (realBattery == null)
                {
                    Debug.LogError($"Error: Token {tokenInSlot.name} is not linked to a Real Battery!");
                    continue;
                }

                if (realBattery.currentAmps != slot.correctAmps)
                {
                    allCorrect = false;
                    Debug.Log($"Mismatch at {slot.name}: Expected {slot.correctAmps} Amps, got {realBattery.currentAmps}");
                }

                if (realBattery.currentVolts != slot.correctVolts)
                {
                    allCorrect = false;
                    Debug.Log($"Mismatch at {slot.name}: Expected {slot.correctVolts} Volts, got {realBattery.currentVolts}");
                }
            }
            else
            {
                Debug.LogWarning($"Slot {slot.name} is empty!");
                allCorrect = false;
            }
        }
        if (allCorrect)
        {
            Debug.Log("WIN!");
            if (statusText != null)
            {
                statusText.text = "SYSTEM ONLINE";
                statusText.color = Color.green;
                await System.Threading.Tasks.Task.Yield();
                SceneManager.LoadScene("PressureSync");
            }   
        }
        else
        {
            Debug.Log("FAIL");
            if (statusText != null)
            {
                statusText.text = "ERROR: MISMATCH";
                statusText.color = Color.red;
            }
        }
    }
}