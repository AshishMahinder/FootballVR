using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRFootball
{

    public class KeyFrame : MonoBehaviour
    {
        public int frame;
        public float time;
        public TimeObject[] objects;
        public UnityEvent unityEvent;
        public bool useEvent;

        public void Setup(float fps)
        {
            objects = GetComponentsInChildren<TimeObject>();
            time = frame/fps;
        }

        public void CallEvent()
        {
            if (useEvent)
            {
                unityEvent.Invoke();
               // Debug.Break();
            }
        }

    }
}