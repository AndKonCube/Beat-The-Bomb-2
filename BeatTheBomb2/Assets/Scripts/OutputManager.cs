using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OutputManager : MonoBehaviour
{
    [Header("The Target Display")]
    public TMP_Text totalOutputText; // Drag the text that says "Total Output"

    [Header("The Batteries")]
    public List<BatteryUnit> batteries; // Drag your 4 battery objects here

    [Header("Game Settings")]
    public int targetScore; // The number players need to reach
    private bool gameWon = false;

    void Start()
    {
        StartNewGame();
    }

    void StartNewGame()
    {
        // Pick a random target number (e.g., between 10 and 30)
        targetScore = Random.Range(10, 30);
        
        UpdateTargetText();
        gameWon = false;
    }

    public void CheckTotalOutput()
    {
        if (gameWon) return; // Stop checking if already won

        int currentSum = 0;

        // Loop through all batteries and add up their values
        foreach (BatteryUnit battery in batteries)
        {
            currentSum += battery.currentValue;
        }

        // Check if the sum matches the target
        if (currentSum == targetScore)
        {
            WinGame();
        }
    }

    void UpdateTargetText()
    {
        totalOutputText.text = "Target Output: " + targetScore.ToString();
        totalOutputText.color = Color.white;
    }

    void WinGame()
    {
        gameWon = true;
        totalOutputText.text = "SYSTEM ONLINE (WIN!)";
        totalOutputText.color = Color.green;
        Debug.Log("Puzzle Solved!");
    }
}