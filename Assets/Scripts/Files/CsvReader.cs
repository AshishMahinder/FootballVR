using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{
    public class CsvReader
    {
        const string fileName = "football_trajectory";

        public static List<Vector3> GetPoints ()
        {
            string path = Application.dataPath + "/" + fileName + ".csv";
            string fileData = System.IO.File.ReadAllText(path);
            string[] lines = fileData.Split("\n"[0]);
            List<Vector3> positions = new List<Vector3>();
            for(int i = 1; i < lines.Length; i++)
            {
                Debug.Log("Line: " + lines[i]);
                string[] lineData = (lines[i].Trim()).Split(","[0]);
                if (lineData.Length < 4)
                    continue;
                Vector3 pos = Vector3.zero;
                float x = 0;
                if( float.TryParse(lineData[1], out x))
                {
                    pos.x = x;
                }
                else
                {
                    Debug.LogError("Erro in " + fileName + "x: " + lineData[i] + " is not a float value");
                }
                float y = 0;
                if (float.TryParse(lineData[2], out y))
                {
                    pos.z = y;
                }
                else
                {
                    Debug.LogError("Erro in " + fileName + " y: " + lineData[i] + " is not a float value");
                }
                float z = 0;
                if (float.TryParse(lineData[3], out z))
                {
                    pos.y = z;
                }
                else
                {
                    Debug.LogError("Erro in " + fileName + " z: " + lineData[i] + " is not a float value");
                }
                pos = pos / 100;
                positions.Add(pos);
            }
            return positions;
        }

    }

}