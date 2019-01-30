using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Block.States;

namespace Environment
{
    public class WallBuild : MonoBehaviour {
        // Use this for initialization
        public GameObject blockPrefab = null;
        public string wallPng;
        void Start()
        {
            var image = Resources.Load<Texture2D>(wallPng);
            float y = 0.5f;
            float z = 0.0f;
            // i is col, j is row
            for (int i = 0; i < image.width; i++)
                for (int j = 0; j < image.height; j++)
                {
                    GameObject go = Instantiate(blockPrefab, new Vector3(0, y + j, z + i), Quaternion.identity) as GameObject;
                    go.transform.parent = transform;
                    Color pixel = image.GetPixel(i, j);
                    // if it's a white color then just debug...
                    if (pixel != Color.white)
                        go.GetComponent<BlockState>().SetAsTrigger();
                }

        }
        
        // Update is called once per frame
        void Update () {
            
        }
    }
}