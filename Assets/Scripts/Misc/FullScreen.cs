using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class FullScreen : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Screen.fullScreen = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Screen.fullScreen = false;
                if (Application.isEditor)
                {
                    Debug.Break();
                }
            }
        }
    }
}