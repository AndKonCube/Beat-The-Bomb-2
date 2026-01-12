using UnityEngine;

public class DisplayActivator : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Checking for displays...");
        // Loop through all connected monitors and activate them
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}