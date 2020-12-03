using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFootball
{

    public class PointTest : MonoBehaviour
    {

        public Transform ball;
        [SerializeField]
        float time = 0;
        List<Vector3> points = new List<Vector3>();
        Vector3 pos;
        public bool move = true;
        public bool launchBall = true;
        public float duration = 4;
        const float g = -9.8f;
        public bool useWeight = false;
        [SerializeField]
        float airDrag;

        private void Start()
        {
            //test points
            /*
            points.Add(new Vector3(0, 0, 0));
            points.Add(new Vector3(35.9f, 110.7f, 74.3f));
            points.Add(new Vector3(44.3f, 247.2f, 114.5f));
            points.Add(new Vector3(52.1f, 377.9f, 149.3f));
            points.Add(new Vector3(56.3f, 507.7f, 174.5f));
            points.Add(new Vector3(69.7f, 649.3f, 177.9f));
            points.Add(new Vector3(80.3f, 766.5f, 196.5f));
            points.Add(new Vector3(86.3f, 893.4f, 184.1f));
    
            for(int i = 0; i < points.Count; i++)
            {
                Vector3 v = points[i];
                v.y = points[i].z;
                v.z = points[i].y;
               // v.x  = -v.x;
                points[i] = v/100f;
            }
            */

            points = CsvReader.GetPoints();
            pos = ball.position;

            if (!move)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    Instantiate(ball.gameObject, pos + (points[i]), ball.transform.rotation);
                }
            }

            // airDrag = GetAirDrag();
            Parameters p = new Parameters();
            string str = JsonUtility.ToJson(p);
            Debug.Log(str);
        }

        float GetAirDrag2()
        {
            float dt = 0.1f;
            Vector3 delta = points[1] - points[0];
            delta.y = 0;
            float speed = delta.magnitude / dt;
            float drag = 0;
            for (int i = 2; i < points.Count; i++)
            {
                delta = points[i] - points[0];
                delta.y = 0;
                float speed2 = delta.magnitude / dt;
                float d = GetDrag(speed, speed2, (i - 1) * dt);
                drag += d;
                Debug.Log("Drag" + i + ":" + d);
            }
            drag = drag / (points.Count - 2);
            return drag;
;        }

        float GetAirDrag()
        {
            float drag = 0;
            float dt = 0.1f;
            for (int i = 1; i < points.Count - 1; i++)
            {
                Vector3 delta1 = points[i] - points[i - 1];
                Vector3 delta2 = points[i + 1] - points[i];
                delta1.y = 0;
                delta2.y = 0;
                Vector3 velocity1 = delta1 / dt;
                Vector3 velocity2 = delta2 / dt;
                Vector3 acc = (velocity2 - velocity1) / dt;
                float d = acc.magnitude / Mathf.Pow(velocity1.magnitude, 2);
                drag += d;
                Debug.Log("V"+ i+ ":" + velocity1.magnitude + " drag " + d);
            }
            drag = drag / (points.Count - 2);
            return drag;
        }

        private void Update()
        {
            if(!move)
                return;
            if(time == 0 && launchBall)
            {
                LaunchBall();
            }
            time += Time.deltaTime;
            float t = time * 10;
            float f = Mathf.Floor(t);
            float r = t - f;
           // Debug.Log(" > " + f + " / " + r + " / " + time);
            if(f < points.Count - 1)
            {
                Vector3 p = Vector3.Lerp(points[(int) f], points[(int)f + 1], r);
                ball.position = pos + p;
               // Debug.Log("pos " + ball.position);
            }
            else if(time > duration){
                time = 0;
            }
        }

        void LaunchBall()
        {
            GameObject obj = Instantiate(ball.gameObject, pos, ball.rotation);
            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.isKinematic = false;
            obj.GetComponent<Collider>().enabled = true;
            body.velocity = GetStartVelocity();
            obj.GetComponent<MeshRenderer>().enabled = true;
            Destroy(obj, duration);
        }

        Vector3 GetStartVelocity()
        {
            float angle = GetShotAngle();
            float speedY = GetSpeedY();
            float fwdSpeed = GetForwardSpeed();
            Debug.Log("Fwd Speed: " + fwdSpeed);
            Vector3 velocity = Quaternion.Euler(0, angle, 0) *Vector3.forward;
            velocity.y = 0;
            velocity = velocity.normalized * fwdSpeed;
            velocity.y = speedY;
            return velocity;
        }

        float GetShotAngle()
        {
            float angle = 0;
            float total = 0;
            for(int i = 1; i < points.Count; i++)
            {
                Vector3 delta = points[i] - points[0];
                delta.y = 0;
                angle += Vector3.SignedAngle(Vector3.forward, delta, Vector3.up)*delta.magnitude;
                total += delta.magnitude;
            }
            //angle = angle / (points.Count - 1);
            angle = angle / total;
            return angle;
        }

        float GetSpeedY()
        {
            float speed = 0;
            float total = 0;
            for (int i = 1; i < points.Count; i++)
            {
                float t = ((float) i)/ 10f;
                float weight = points.Count - i;
                total += weight;
                float f = GetStartSpeed(points[i].y, t);
                if (useWeight)
                    f *= weight;
                speed += f;
            }
            if(useWeight)
                return speed / total;
            else return speed / (points.Count - 1);

        }

        float GetStartSpeed(float height, float time)
        {
            return (height - (g * Mathf.Pow(time, 2) / 2))/time;
        }

        float GetForwardSpeed()
        {
            return GetMaxSpeed();
           // return GetAvgSpeed();
             
            //Vector3 delta = points[1] - points[0];
                // delta.y = 0;
                //float speed = delta.magnitude / 0.1f;


                // speed = GetStartForwardSpeed(speed);
                //  float previousSpeed = GetPreviousSpeed(speed);
                //  Debug.Log("PRevious Speed " + speed);
                //  Debug.Log("SPEED " + speed);
             //   return speed;
        }

        float GetAvgSpeed()
        {
            float speed = 0;
            float dt = 0.1f;
            for (int i = 1; i < points.Count; i++)
            {
                Vector3 delta = points[i] - points[i - 1];
                delta.y = 0;
                float s = delta.magnitude / dt;
                speed += s;
            }
            // Debug.Log("Total " + speed);
            speed = speed / (points.Count - 1);
            //Debug.Log("Speed " + speed);
            return speed;
        }

        float GetMaxSpeed()
        {
            float maxSpeed = 0;
            float dt = 0.1f;
            for (int i = 1; i < points.Count; i++)
            {
                Vector3 delta = points[i] - points[i - 1];
                delta.y = 0;
                float s = delta.magnitude / dt;
                if (s > maxSpeed)
                    maxSpeed = s;
            }
            return maxSpeed;
        }

        float GetStartForwardSpeed(float speed)
        {
            float dt = 0.1f;
            float resistence = airDrag * dt;
            float a = -resistence;
            float b = 1;
            float c = -speed;
            return GetBhaskara(a, b, c);
        } 

        float GetBhaskara(float a, float b, float c)
        {
            float s = Mathf.Sqrt((b*b) - (4 * a * c));
            float x = (-b + s) / 2 * a;
            if (x > 0)
                return x;
            return (-b - s) / 2 * a;
        }

        float GetPreviousSpeed(float speed)
        {
            float deltaSpeed = 0.1f;
            float startSpeed = speed;
            float drag = ball.GetComponent<Rigidbody>().drag;
            Debug.Log("Body Drag " + drag);
            float dt = 0.1f;
            for(int i = 0; i < 10000; i++)
            {
                startSpeed += deltaSpeed;
                float finalSpeed = startSpeed - drag * Mathf.Pow(startSpeed, 2) * dt;
                Debug.Log("Start Speed " + startSpeed + " final speed " + finalSpeed);
                if (finalSpeed >= speed)
                    return startSpeed;
            }
            return speed;
        }

        public float GetDrag(float speed1, float speed2, float duration)
        {
            float min = 0.0001f;
            float dt = Time.fixedTime;
            for (float drag = min; drag < 0.01f; drag += min)
            {
                float speed = speed1;
                for(float t = 0; t < duration; t += dt)
                {
                    speed = speed - drag * Mathf.Pow(speed, 2);
                }
                if (speed <= speed2)
                    return drag;
            }
            return 0;
        }

    }
}
