using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block
{
    /// <summary>
    /// Maintains the stack of blocks on the player and provides functions for returning data about that stack
    /// </summary>
    public class BlockStack
    {
        private List<Stackable> Stack;
        
        public BlockStack()
        {
            Stack = new List<Stackable>();
        }

        public void AddStackable(Stackable stackable)
        {
            this.Stack.Add(stackable);
        }

        public void RemoveStackable(Stackable stackable)
        {
            this.Stack.Remove(stackable);
        }

        public float GetStackHeight()
        {
            float stackHeight = 0.0F;
            Stack.ForEach((Stackable stackable) => stackHeight += stackable.GetHeight());
            return stackHeight;
        }

        public Stackable GetLastAdded()
        {
            if (this.Stack.Count > 0)
            {
                return this.Stack[this.Stack.Count - 1];
            }
            else
            {
                return null;
            }
        }
    }
}