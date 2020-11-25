using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OldOdin;

namespace TheGirl
{

    public class LookToTarget : MonoBehaviour
    {
        public Transform target;
        public Transform head;
        float maxAngle = 70;
        public Vector3 Foward = Vector3.up;
        public Vector3 Right = Vector3.forward;
        Quaternion[] rotations;
        float headAngularSpeed = 20;
        float maxRandomAngle = 10;
        float randomAngle;
        float randomAngleTime;
        float randomAngleDelay = 3;
        [SerializeField]
        Transform[] eyes;
        Animazing animazing;

        private void Start()
        {
            if (head == null)
            {
                head = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
            }
            if (target == null)
            {
                if (Camera.main != null)
                    target = Camera.main.transform;
                else target = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rotations = new Quaternion[4];
            SaveRotatios();
            if(eyes.Length == 0 && GetComponent<Animator>().isHuman && false)
            {
                eyes = new Transform[2];
                eyes[0] = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftEye);
                eyes[1] = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightEye);
            }
            animazing = GetComponent<Animazing>();
        }

        private void LateUpdate()
        {
          //  if (!animazing.CanPlay(9))
           //     return;
            if(Time.time >= randomAngleTime)
            {
                randomAngleTime = Time.time + randomAngleDelay;
                randomAngle = Random.Range(-maxRandomAngle, maxRandomAngle);
            }
            Look();
        }

        void Look()
        {
            Vector3 delta = target.position - head.position;
            Vector3 d = delta;
            d.y = 0;
            Vector3 fwd = head.TransformDirection(Foward);
            float angle = Vector3.SignedAngle(fwd, d, Vector3.up);
          //  angle += randomAngle;
            if (angle > maxAngle)
                angle = maxAngle;
            if (angle < -maxAngle)
                angle = -maxAngle;
            Rotate(Vector3.up, angle);

            fwd = head.TransformDirection(Foward);
            angle = Vector3.SignedAngle(fwd, delta, head.TransformDirection(Right));
            if (angle > maxAngle)
                angle = maxAngle;
            if (angle < -maxAngle)
                angle = -maxAngle;
            Rotate(head.TransformDirection(Right), angle);
            LertpRotations();
            SaveRotatios();
            EyesLook();
        }

        void EyesLook()
        {
            for(int i = 0; i < eyes.Length; i++)
            {
                if(eyes[i] != null)
                {
                    eyes[i].right =  -Camera.main.transform.position + eyes[i].transform.position;
                }
            }
        }


        void Rotate(Vector3 direction, float angle)
        {
            head.Rotate(direction, angle * 0.4f, Space.World);
            head.parent.Rotate(direction, angle * 0.2f, Space.World);
            head.parent.parent.Rotate(direction, angle * 0.3f, Space.World);
            head.parent.parent.parent.Rotate(direction, angle * 0.1f, Space.World);
        }

        void SaveRotatios()
        {
            for(int i = 0; i < rotations.Length; i++)
            {
                Transform t = Get(i);
                rotations[i] = t.localRotation;
            }
        }

        Transform Get(int i)
        {
            Transform t = head;
            for (int j = 0; j < i; j++)
            {
                t = t.parent;
            }
            return t;
        }

        void LertpRotations()
        {
            for (int i = 0; i < rotations.Length; i++)
            {
                Transform t = Get(i);
                t.localRotation = Quaternion.RotateTowards(rotations[i], t.localRotation, headAngularSpeed * Time.deltaTime);
            }
        }
    }
}
