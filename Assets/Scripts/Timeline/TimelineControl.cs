using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{
    [ExecuteInEditMode]
    public class TimelineControl : MonoBehaviour
    {
        public bool run;
        public bool isVisible = true;

        private void Update()
        {
            if (run)
            {
                run = false;
                isVisible = !isVisible;
                for(int i = 0; i < transform.childCount; i++)
                {
                    Transform t = transform.GetChild(i);
                    t.GetChild(0).gameObject.SetActive(true);
                    for(int j = 1; j < t.childCount; j++)
                    {
                        t.GetChild(j).gameObject.SetActive(isVisible);
                    }
                }
            }
        }
    }

}
