using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OldOdin;

namespace VRFootball
{

    public class AnimationSequence : MonoBehaviour
    {
        public AnimationClip idle;
        Animazing animazing;
        float priority = 0;
        float time;
        [SerializeField]
        AnimationNode[] nodes;

        private void Awake()
        {
            animazing = GetComponent<Animazing>();
            animazing.SetLayerDefaultAnimation(0, idle);
            nodes = GetComponentsInChildren<AnimationNode>();
            for(int i = 0; i < nodes.Length; i++)
            {
                nodes[i].priority = i;
            }
        }

        private void Update()
        {
            /*
            time += Time.deltaTime;
            int index = GetIndex();
            if(index >= 0)
            {

            }
            */
        }

        int GetIndex()
        {
            int frame = (int)Mathf.Floor(time * 25f);
            for (int i = 0; i < nodes.Length; i++)
            {
                if (frame <= nodes[i].frame)
                {
                    return i - 1;
                }
            }
            return -1;
        }

    }

}