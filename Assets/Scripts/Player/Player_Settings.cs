using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player_Settings : MonoBehaviour
    {
        /// <summary>
        /// Default is 150.0f
        /// </summary>
        public float TurnSpeed = 150.0f;
        
        /// <summary>
        /// Default is 3.0f
        /// </summary>
        public float WalkSpeed = 3.0f;
        
        /// <summary>
        /// Default is true
        /// </summary>
        public bool BlocksStackUp = true;
    }
}