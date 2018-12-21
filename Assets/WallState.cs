using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallState : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var block in GetComponentsInChildren<BlockState>()) {
            if (block.IsSatisfied() == false)
                return;
        }
        foreach (var celebrate in GetComponentsInChildren<Celebrate>()){
            celebrate.Confetti();
        }
    }
}
