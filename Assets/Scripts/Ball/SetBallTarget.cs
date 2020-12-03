using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGirl;

namespace VRFootball
{

    public class SetBallTarget : MonoBehaviour
    {
        LookToTarget look;
        Transform t;

        private void Awake()
        {
            look = GetComponent<LookToTarget>();
            t = new GameObject("ref").transform;
            SetTarget();
        }

        private void Update()
        {
            SetTarget();
        }

        void SetTarget()
        {
            if (Ball.GetInstance() != null)
                t.transform.position = Ball.GetInstance().transform.position + 2.8f*Vector3.up;
            look.target = t;
        }

    }
}