using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class Timeline : MonoBehaviour
    {
        public float fps = 25;
        public KeyFrame[] frames;
        public int startFrame = 3;
        float time;
        Transform[] objects;
        IAnimator[] animators;
        int lastIndex = -1;
        public bool destroyAtTheEnd;
        
        private void Awake()
        {
            frames = GetComponentsInChildren<KeyFrame>();
            for(int i = 0; i < frames.Length; i++)
            {
                frames[i].frame -= startFrame;
                frames[i].Setup(fps);
                frames[i].gameObject.SetActive(false);
            }
            objects = new Transform[frames[0].objects.Length];
            animators = new IAnimator[frames[0].objects.Length];
            for(int i = 0; i < objects.Length; i++)
            {
                var prefab = frames[0].objects[i].gameObject ;
                var obj = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
                obj.SetActive(true);
                objects[i] = obj.transform;
                animators[i] = obj.GetComponentInChildren<IAnimator>();
            }
        }

        private void Update()
        {
            time += Time.deltaTime;
            int index = GetIndex();
         //   Debug.Log("index = " + index);
            if(index >= 0)
            {
                if (index != lastIndex)
                {
                    frames[index].CallEvent();
                    lastIndex = index;
                    for (int i = 0; i < frames[0].objects.Length; i++)
                    {
                        ActionAnimation a = frames[index].objects[i].GetComponent<ActionAnimation>();
                        if(a != null)
                        {
                            animators[i].Play(a.clip, a.priority, a.changeIdle);
                            if (a.runTransition >= 0)
                                animators[i].SetRunTransition(a.runTransition);
                        }
                    }
                }
                float t = time - frames[index].time;
                float total = frames[index + 1].time - frames[index].time;
                float rate = t / total;
              //  Debug.Log("Rate " + rate);
                for(int i = 0; i < frames[0].objects.Length; i++)
                {
                    

                    objects[i].position = Vector3.Lerp(frames[index].objects[i].transform.position, frames[index + 1].objects[i].transform.position, rate);
                    objects[i].rotation = Quaternion.Lerp(frames[index].objects[i].transform.rotation, frames[index + 1].objects[i].transform.rotation, rate);


                    if (animators[i] != null)
                    {
                        Vector3 delta = frames[index + 1].objects[i].transform.position - frames[index].objects[i].transform.position;
                        animators[i].SetVelocity(delta / total);
                    }
                }
            }
            else
            {
                for (int i = 0; i < frames[0].objects.Length; i++)
                {
                    if (animators[i] != null)
                    {
                        animators[i].SetVelocity(Vector3.zero);
                    }
                    ActionAnimation a = frames[frames.Length - 1].objects[i].GetComponent<ActionAnimation>();
                    if (a != null)
                    {
                        animators[i].Play(a.clip, a.priority, a.changeIdle);
                        if (a.runTransition >= 0)
                            animators[i].SetRunTransition(a.runTransition);
                    }
                }
                enabled = false;
                frames[frames.Length - 1].CallEvent();
                if (destroyAtTheEnd)
                {
                    for(int i = 0; i < objects.Length; i++)
                    {
                        Destroy(objects[i].gameObject);
                    }
                }
            }
        }

        int GetIndex()
        {
            //Debug.Log("Time " + time);
            for(int i = 1; i < frames.Length; i++)
            {
              //  Debug.Log("frame time " + frames[i].time);
                if(time <= frames[i].time)
                {
                    return i - 1;
                }
            }
            return -1;
        }
    }
}