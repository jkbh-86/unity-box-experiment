using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEffects
{
    [RequireComponent(typeof(Pixelate))]
    public class PixelateTransitions : MonoBehaviour {
        public float transitionTime = 3.0f;
        public int pixelLevel = 2;
        public int maxPixelLevel = 10;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(PixelateTransitionIn());
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
            GetComponent<Pixelate>().pixelSizeX = maxPixelLevel;
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / transitionTime;
                GetComponent<Pixelate>().pixelSizeX = (int)Mathf.Lerp(maxPixelLevel, pixelLevel, t);
                yield return null;
            }
        }
    }
}