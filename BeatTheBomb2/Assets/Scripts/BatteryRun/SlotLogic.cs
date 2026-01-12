using UnityEngine;

/// <summary>
/// Represents a physical slot on the map that holds an answer key (Amps and Volts) 
/// and detects which map token is currently placed inside it.
/// </summary>
public class SlotLogic : MonoBehaviour
{
    [Header("The Answer Key for this Slot")]
    public int correctAmps;
    public int correctVolts; 

    /// <summary>
    /// Searches the children of this object to find and return the MapToken currently occupying the slot.
    /// </summary>
    /// <returns>The MapToken component if found, otherwise null.</returns>
    public MapToken GetCurrentToken()
    {
        return GetComponentInChildren<MapToken>();
    }
}