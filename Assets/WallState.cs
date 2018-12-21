using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallState : MonoBehaviour {
    public float transitionTime = 3.0f;
    public int pixelLevel = 2;
    public int maxPixelLevel = 20;
    // Use this for initialization
    void Start () {
        StartCoroutine(PixelateTransitionIn());
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
        StartCoroutine(PixelateTransitionOut());
    }

    public IEnumerator PixelateTransitionOut()
    {
        var current = Camera.main.GetComponent<Pixelate>().pixelSizeX;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / transitionTime;
            Camera.main.GetComponent<Pixelate>().pixelSizeX = (int)Mathf.Lerp(current, maxPixelLevel, t);
            yield return null;
        }
    }

    public IEnumerator PixelateTransitionIn()
    {
        Camera.main.GetComponent<Pixelate>().pixelSizeX = 20;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / transitionTime;
            Camera.main.GetComponent<Pixelate>().pixelSizeX = (int)Mathf.Lerp(maxPixelLevel, pixelLevel, t);
            yield return null;
        }
    }
}
