using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{
    [ExecuteInEditMode]
    public class StadioMarks : MonoBehaviour
    {

        public bool run = false;
        public int total = 6;
        public GameObject mark;
        public Material[] materials;
        [SerializeField]
        GameObject floor;
        int index = 0;

        private void Update()
        {
            if (run)
            {
                run = false;
                Create();
            }
        }

        void Create()
        {
            index = 0;
            if(floor != null)
            {
                DestroyImmediate(floor);
            }
            floor = new GameObject("floor");
            floor.transform.position = mark.transform.position;
            floor.transform.rotation = mark.transform.rotation;
            Vector3 position = mark.transform.position;
            for (int i = 0; i < total; i++)
            {
                position += Vector3.forward*mark.transform.localScale.z;
                GameObject obj = Instantiate(mark, position, mark.transform.rotation);
                obj.transform.parent = floor.transform;
                index++;
                if (index >= materials.Length)
                {
                    index = 0;
                }
                obj.GetComponent<MeshRenderer>().material = materials[index];
            }
        }
    }
}