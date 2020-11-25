using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class ActionAnimation : MonoBehaviour
    {
        public AnimationClip clip;
        public float priority = 2;
        public bool changeIdle;
        public float runTransition = -1;
    }

}