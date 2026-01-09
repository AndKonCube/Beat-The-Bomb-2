using UnityEngine;

public class SlotLogic : MonoBehaviour
{
    [Header("The Answer Key for this Slot")]
    public int correctAmps; // The target for the LEFT side (Amperemeter)
    public int correctVolts; // The target for the RIGHT side (Voltmeter)

    // Helper to find the Token sitting here
    public MapToken GetCurrentToken()
    {
        return GetComponentInChildren<MapToken>();
    }
}