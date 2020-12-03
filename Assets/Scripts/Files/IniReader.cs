using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace VRFootball {

    public class IniReader : MonoBehaviour
    {
        public Parameters parameters;

        private void Awake()
        {
            string path = Application.dataPath + "/ini.txt";
            string fileData = System.IO.File.ReadAllText(path);
            try
            {
                parameters = JsonUtility.FromJson<Parameters>(fileData);
            }
            catch
            {
                Debug.LogError("Error reading " + path);
                return;
            }
            GameObject cam = Camera.main.gameObject;
            var contrast = cam.GetComponent<ContrastStretch>();
            contrast.limitMinimum = parameters.contrast;
            var blur = cam.GetComponent<BlurOptimized>();
            blur.enabled = parameters.blur > 0;
            blur.blurSize = parameters.blur*10;
            if (parameters.blur < 0.75f)
                blur.blurIterations = 1;
            else blur.blurIterations = 2;
            Time.timeScale = parameters.animationSpeed;
           //cam.GetComponent<Imageed>
        }

    }

}
