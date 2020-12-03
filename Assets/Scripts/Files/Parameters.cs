using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{
    [System.Serializable]
    public class Parameters 
    {
        public float contrast;
        public float blur;
        public int[] visiblePlayers1;
        public int[] visiblePlayers2;
        public bool stress;
        public Vector3 target;
        public float animationSpeed;
        public bool oneEye;
        
    }
}