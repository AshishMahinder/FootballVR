using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class Follow : MonoBehaviour
    {
        public Transform target;

        private void LateUpdate()
        {
            if(target != null)
            {
                Vector3 pos = target.position;
                pos.y = transform.position.y;
                transform.position = pos;
            }
        }
    }

}