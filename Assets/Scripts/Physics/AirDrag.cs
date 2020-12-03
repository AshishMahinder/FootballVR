using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class AirDrag : MonoBehaviour
    {
        public float drag = 0f;

        Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!body.isKinematic && drag > 0)
            {
                Vector3 velocity = body.velocity;
                Vector3 force = drag * velocity.normalized * Mathf.Pow(velocity.magnitude, 2);
                body.AddForce(-force, ForceMode.VelocityChange);
            }
        }

    }
}