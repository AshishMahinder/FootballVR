using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OldOdin;
using TheGirl;

namespace VRFootball
{
    [RequireComponent(typeof(Animazing))]
    public class LocomotionAnimatior : MonoBehaviour, IAnimator
    {
        public AnimationClip idle;
        public AnimationClip walk;
        public AnimationClip run;
        public AnimationClip runBack;
        public AnimationClip runFast;
        public float speed;
        Animazing animazing;
        public float priority = 1;
        public Vector3 velocity;
        public float runTransition = 0.3f;
        public float actionTransition = 0.2f;
        public float runFastFrame = Mathf.Infinity;

        private void Start()
        {
            animazing = GetComponent<Animazing>();
            animazing.SetLayerDefaultAnimation(0, idle, 0.3f);
          //  transform.localScale *= 1.16f;
        }

        public void NoAnimazing()
        {
            animazing.enabled = false;
            GetComponent<AnimationLayer>().enabled = false;
            LookToTarget look = GetComponent<LookToTarget>();
            if (look != null)
                look.enabled = false;
            enabled = false;
            transform.parent = null;
            GetComponent<Animator>().applyRootMotion = true;
        }

        private void Update()
        {
            if (Time.time*25f >= runFastFrame)
            {
                run = runFast;
                runFastFrame = Mathf.Infinity;
            }
            if (IsBack())
            {
                if(speed >= 0.5f)
                {
                    animazing.Play(runBack, priority, 0.3f);
                }
            }
            else if(speed >= 2f)
            {
                animazing.Play(run, priority, runTransition);
            }else if(speed >= 0.4f)
            {
                animazing.Play(walk, priority, 0.3f);
            }
        }

        bool IsBack()
        {
            if (speed == 0)
                return false;
            float angle = Vector3.SignedAngle(transform.forward, velocity, Vector3.up);
            return Mathf.Abs(angle) > 120;
        }

        public void Play(AnimationClip clip, float priority, bool changeIdle)
        {
            if (changeIdle)
            {
                GetComponentInChildren<Animator>().applyRootMotion = true;
                animazing.SetLayerDefaultAnimation(0, clip);
            }
            else animazing.Play(clip, priority, actionTransition);
        }

        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
            this.speed = velocity.magnitude;
        }

        public void SetRunTransition(float transition)
        {
            this.runTransition = transition;
        }
    }
}