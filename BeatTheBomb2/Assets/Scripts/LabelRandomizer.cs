using UnityEngine;
using TMPro; // Required for Text Mesh Pro
using System.Collections.Generic;

public class LabelRandomizer : MonoBehaviour
{
    [Header("Drag your 4 Battery Objects here")]
    public List<RectTransform> objectsToShuffle;

    void Start()
    {
        ShufflePositions();
    }

    public void ShufflePositions()
    {
        // 1. Store the original positions of the 4 "slots"
        List<Vector2> validPositions = new List<Vector2>();

        foreach (RectTransform item in objectsToShuffle)
        {
            // anchoredPosition is the position relative to the Canvas/Panel (X, Y)
            validPositions.Add(item.anchoredPosition);
        }

        // 2. Shuffle the list of positions using Fisher-Yates algorithm
        for (int i = 0; i < validPositions.Count; i++)
        {
            Vector2 temp = validPositions[i];
            int randomIndex = Random.Range(i, validPositions.Count);
            validPositions[i] = validPositions[randomIndex];
            validPositions[randomIndex] = temp;
        }

        // 3. Assign the shuffled positions back to the objects
        for (int i = 0; i < objectsToShuffle.Count; i++)
        {
            objectsToShuffle[i].anchoredPosition = validPositions[i];
        }
    }
}