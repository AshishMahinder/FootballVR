using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRFootball
{

    public class CurvedMovement : MonoBehaviour
    {
        public float duration= 3;
        public float disapearTime = Mathf.Infinity;
        public AnimationCurve X;
        public AnimationCurve Y;
        public AnimationCurve Z;
        public Vector3 max;
        float time;
        public bool loop = true;
        public UnityEvent unityEvent;

        public void Update()
        {
            time += Time.deltaTime;
            float rate = time / duration;
            if (time >= disapearTime)
            {
                rate = 2;
                if (!loop)
                {
                    if (unityEvent != null)
                        unityEvent.Invoke();
                    Destroy(gameObject);
                    //Debug.Break();
                }
            }
            if (rate <= 1) {
                transform.localPosition = new Vector3(X.Evaluate(rate)*max.x, Y.Evaluate(rate)*max.y, Z.Evaluate(rate)*max.z);
            }
            else if(loop)
            {
                time = 0;
            }
        }

    }
}