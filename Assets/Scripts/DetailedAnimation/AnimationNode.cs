using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OldOdin;

namespace VRFootball
{
    [ExecuteInEditMode]
    public class AnimationNode : MonoBehaviour
    {
        public AnimationClip clip;
        public int frame;
        public float transition = 0.2f;
        public float priority = 0;
        Animazing animazing;
        public float angle = 0;
        public float angularSpeed = 360;

        private void Awake()
        {
            animazing = GetComponentInParent<Animazing>();
        }

        private void Update()
        {
            if (clip != null)
                gameObject.name = clip.name;
            if (!Application.isPlaying)
                return;
            if(Time.time*25 >= frame)
            {
                if (animazing.CanPlay(priority + 1))
                {
                    if (Mathf.Abs(angle) > 0.1f)
                    {
                        float maxAngle = angularSpeed * Time.deltaTime;
                        float deltaAngle = angle;
                        if (deltaAngle > maxAngle)
                            deltaAngle = maxAngle;
                        else if (deltaAngle < -maxAngle)
                            deltaAngle = -maxAngle;
                        animazing.transform.Rotate(Vector3.up, deltaAngle);
                        angle -= deltaAngle;
                    }
                    animazing.Play(clip, priority, transition);
                    if (!clip.isLooping)
                    {
                        enabled = false;
                    }
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }

}
