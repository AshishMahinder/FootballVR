using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class Rolling : MonoBehaviour, IAnimator
    {
        public Transform ball;
        Vector3 velocity;
        public float angularSpeed;

        public void Play(AnimationClip clip, float priority, bool changeIdle)
        {
            
        }

        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
            angularSpeed = velocity.magnitude * 270;
        }

        public void Update()
        {
            Vector3 axis = Quaternion.Euler(0, 90, 0) * velocity;
            float angle = angularSpeed * Time.deltaTime;
            ball.Rotate(axis, angle);
        }

        public void SetRunTransition(float transition)
        {
            
        }
    }
}