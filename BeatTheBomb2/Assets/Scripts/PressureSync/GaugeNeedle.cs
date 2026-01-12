using UnityEngine;

public class GaugeNeedle : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float minAngle = -90f; // Far left
    [SerializeField] private float maxAngle = 90f;  // Far right
    [SerializeField] private float smoothingSpeed = 5f; // How fast it moves to the spot

    private float targetAngle;

    // This runs every time the GameObject is activated (Screen 2 opens)
    private void OnEnable()
    {
        // 1. Pick a random value between 0.0 and 1.0
        float randomPercent = Random.Range(0f, 1f);
        
        // 2. Calculate the rotation angle based on that random value
        // Mathf.Lerp calculates the point between min and max based on the percent
        targetAngle = Mathf.Lerp(minAngle, maxAngle, randomPercent);

        // Optional: Snap instantly on open? Uncomment line below.
        // transform.localRotation = Quaternion.Euler(0, 0, targetAngle); 
    }

    private void Update()
    {
        // Smoothly rotate the needle towards the random target
        // We assume the needle rotates around the Z axis
        float currentAngle = transform.localEulerAngles.z;
        
        // Unity Euler angles can be tricky (0 to 360), so we use Quaternion.Lerp for safety
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothingSpeed);
    }
}