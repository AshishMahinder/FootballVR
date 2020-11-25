using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class TiltTurn : MonoBehaviour
    {
        public Transform anim;
        Vector3 lastDirection;
        float angularSpeed;
        Quaternion originalRotationç;
        float lerpAngle;

        private void Reset()
        {
            SetAnim();
        }

        private void Start()
        {
            SetAnim();
            lastDirection = transform.forward;
            originalRotationç = anim.localRotation;
        }

        void SetAnim()
        {
            if (anim != null)
                return;
            Animator animator = GetComponentInChildren<Animator>();
            if(animator != null)
            {
                anim = animator.transform;
            }
        }

        private void LateUpdate()
        {
            float angle = Vector3.SignedAngle(lastDirection, transform.forward, Vector3.up);
            lastDirection = transform.forward;
            angularSpeed = angle * Time.deltaTime;
            anim.localRotation = originalRotationç;
            lerpAngle = Mathf.Lerp(lerpAngle, -angle * 3, 0.3f);
            anim.Rotate(anim.forward, lerpAngle);
        }
    }

}