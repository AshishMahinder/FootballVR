using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRFootball
{

    public class FrameCounter : MonoBehaviour
    {
        public float frame;
        public Text label;

        private void Update()
        {
            frame = Time.time * 25;
            if(label != null)
                label.text = "FRAME: " + (int)frame;
        }
    }
}