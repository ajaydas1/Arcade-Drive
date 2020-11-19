using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CopsAI : MonoBehaviour
{
    public Transform Player;
    public float maxSpeed = 120f;
    public float SteerAngle = 35;
    public enum Axel
    {
        front,
        rear
    }
    [Serializable]public struct Wheels{
        public WheelCollider _Collider;
        public Transform _Transfrom;
        public Axel _Axel;
    }
    public List<Wheels> wheels;
    public Vector3 CenterOfMass;
    [Header("Sensors")]
    [SerializeField]private Vector3 FrontSensorStartPos = new Vector3(0f, 0.2f, 1f);
    [SerializeField]private float SensorSize;
    [SerializeField]private float SideSensorsPos;
    [SerializeField]private bool isAvoiding = false;
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = GetComponent<Rigidbody>().centerOfMass-CenterOfMass;
    }
    void FixedUpdate()
    {
        Steering();
        move();
        AnimateWheels();
        Sensors();
    }

    void Steering()
    {
        if(isAvoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(Player.transform.position);
        float SteerSenstivity = (relativeVector.x / relativeVector.magnitude) * SteerAngle;
        foreach (var wheel in wheels)
        {
            if (wheel._Axel == Axel.front)
            {
                wheel._Collider.steerAngle = SteerSenstivity;
            }
        }
    }
    public void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot = Quaternion.identity;
            Vector3 pos = Vector3.zero;
            wheel._Collider.GetWorldPose(out pos, out rot);
            wheel._Transfrom.position = pos;
            wheel._Transfrom.rotation = rot;
        }
    }

    void move()
    {
        foreach (var wheel in wheels)
        {
            wheel._Collider.motorTorque = maxSpeed;
        }
    }
     void Sensors()
     {
        Vector3 SensorsStartpos = transform.position + FrontSensorStartPos;
        SensorsStartpos += transform.forward * FrontSensorStartPos.z;
        SensorsStartpos += transform.up * FrontSensorStartPos.y;
        RaycastHit hit;
        isAvoiding = false;
        float avoidMultiplier = 0;

         float SensorsSideAngle = 30f;
         //Sensor Front
         if(Physics.Raycast(SensorsStartpos, transform.forward, out hit, SensorSize))
         {
            if(!hit.collider.CompareTag("Player"))
            {
            Debug.DrawLine(SensorsStartpos, hit.point);
            isAvoiding = true;
            if(hit.normal.x < 0){avoidMultiplier -= 1;}
            else{avoidMultiplier += 1;}
            
            }
         }
        

        //Sensor left
         SensorsStartpos += transform.right * SideSensorsPos;
         if(Physics.Raycast(SensorsStartpos, transform.forward, out hit, SensorSize))
         {
            if(!hit.collider.CompareTag("Player")){
            Debug.DrawLine(SensorsStartpos, hit.point);
            isAvoiding = true;
            avoidMultiplier += 1f;
            }
         }


         //Sensor left Angle
         else if(Physics.Raycast(SensorsStartpos, Quaternion.AngleAxis(SensorsSideAngle, transform.up) * transform.forward, out hit, SensorSize))
         {
            if(!hit.collider.CompareTag("Player")){
            Debug.DrawLine(SensorsStartpos, hit.point);
            isAvoiding = true;
            avoidMultiplier += 0.8f;
            }
         }
        
        
        //Sensor Right
            SensorsStartpos -= transform.right * SideSensorsPos * 2;
         if(Physics.Raycast(SensorsStartpos, transform.forward, out hit, SensorSize))
         {
            if(!hit.collider.CompareTag("Player")){
            Debug.DrawLine(SensorsStartpos, hit.point);
            isAvoiding = true;
            avoidMultiplier -= 1f;
            }
         }

         //Sensor Right ANgle
        
         else if(Physics.Raycast(SensorsStartpos, Quaternion.AngleAxis(-SensorsSideAngle, transform.up) * transform.forward, out hit, SensorSize))
         {
            if(!hit.collider.CompareTag("Player"))
            {
            Debug.DrawLine(SensorsStartpos, hit.point);
            isAvoiding = true;
            avoidMultiplier -= 0.8f;
            }
         }
         if(isAvoiding)
         {
             foreach (var wheel in wheels)
        {
            if (wheel._Axel == Axel.front)
            {
                wheel._Collider.steerAngle = -avoidMultiplier * SteerAngle;
            }
        }
         }
        
     }

}
