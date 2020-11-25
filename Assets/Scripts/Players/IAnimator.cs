using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public interface IAnimator
    {
        void SetVelocity(Vector3 velocity);
        void Play(AnimationClip clip, float priority, bool changeIdle );
        void SetRunTransition(float transition);
    }
}