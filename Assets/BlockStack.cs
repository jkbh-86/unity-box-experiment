using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maintains the stack of blocks on the player and provides functions for returning data about that stack
/// </summary>
public class BlockStack : MonoBehaviour {
    public List<Stackable> Stack;

    public BlockStack()
    {
        Stack = new List<Stackable>();
    }

    public float GetStackHeight()
    {
        float stackHeight = 0.0F;
        Stack.ForEach((Stackable stackable) => stackHeight += stackable.GetHeight());
        return stackHeight;
    }
}
