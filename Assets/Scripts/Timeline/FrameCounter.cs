using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class FrameCounter : MonoBehaviour
    {
        public float frame;

        private void Update()
        {
            frame = Time.time * 25;
        }
    }
}