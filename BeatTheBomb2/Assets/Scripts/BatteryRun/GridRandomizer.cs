using UnityEngine;
using System.Collections.Generic;

public class GridRandomizer : MonoBehaviour
{
    [Header("The Parent Panel")]
    public Transform panelTransform; // Drag the 'Panel' object here

    void Start()
    {
        ShuffleGrid();
    }

    public void ShuffleGrid()
    {
        // Get all children of the panel
        List<Transform> children = new List<Transform>();
        foreach (Transform child in panelTransform)
        {
            children.Add(child);
        }

        // Shuffle the list of children (Fisher-Yates)
        for (int i = 0; i < children.Count; i++)
        {
            Transform temp = children[i];
            int randomIndex = Random.Range(i, children.Count);
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        // Apply the new order to the Hierarchy
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}