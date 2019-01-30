using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Block.States;
using CustomEffects;

namespace Environment
{
    public class WallState : MonoBehaviour {
        
        // Update is called once per frame
        void Update () {
            foreach (var block in GetComponentsInChildren<BlockState>()) {
                if (block.IsSatisfied() == false)
                    return;
            }
            foreach (var celebrate in GetComponentsInChildren<Celebrate>()){
                celebrate.Confetti();
            }
            StartCoroutine(Camera.main.GetComponent<PixelateTransitions>().PixelateTransitionOut());
        }
    }
}