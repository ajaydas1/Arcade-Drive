using System.Collections.Generic;
using UnityEngine;
using System;

public class RacerAI : MonoBehaviour
{
    //Declaration

    public float maxSpeed = 200f;
    [SerializeField]private Transform path;
    private List<Transform> nodes = new List<Transform>();
    [SerializeField]private int CurrentNode = 0;
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
    void Start()
    {
         Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for(int i=0; i < pathTransforms.Length;i++)
        {
            if(pathTransforms[i] != path.transform){
                nodes.Add(pathTransforms[i]);
            }
        }

        GetComponent<Rigidbody>().centerOfMass = GetComponent<Rigidbody>().centerOfMass-CenterOfMass;
    }

    //Physics Update

    void FixedUpdate()
    {
        
        Steering();
        move();
        AnimateWheels();
    }

    //Features

    void Steering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[CurrentNode].position);
        float Steer = (relativeVector.x / relativeVector.magnitude) * SteerAngle;
        foreach (var wheel in wheels)
        {
            if(wheel._Axel == Axel.front)
            {
                wheel._Collider.steerAngle = Steer;
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
        if (Vector3.Distance(transform.position, nodes[CurrentNode].position) < 3f)
        {
            if(CurrentNode == nodes.Count -1)
            {
                CurrentNode = 0;
            }
            else
            {
                CurrentNode++;
            }
        }
    }
   // void ClosestNode()
   // {
    //    if(Vector3.Distance(transform.position, nearestnode) < 2f)
    //    {
     //   }
    //}
}