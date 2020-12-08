using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

	public enum Axel
{
	front,
	rear
}
[Serializable]
public struct Wheel
{
	public Transform wheelTransform;
	public WheelCollider wheelCollider;
	public Axel axel;
}



public class CarController2 : MonoBehaviour
{
	public InputManager inputs;
	[SerializeField]private float Acceleration = 100;
	[SerializeField]private float Steerangle = 35;
	[SerializeField]private float BrakeForce = 100;
	[SerializeField]private List<Wheel> wheels;
	private Rigidbody _rb;
	[SerializeField]private Vector3 CenterOfMass;

	//[SerializeField]private float RPM;
	[SerializeField]private float maxSpeed = 5f;
	

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass -= CenterOfMass;
		//RPM = maxSpeed / (2 * Mathf.PI * wheels[1].wheelCollider.radius);
    }

    // Update is called once per frame
    void LateUpdate()
    {
		Move();
        animateWheels();
        Brake();
    }

    void Move()
    {
    	foreach (var wheel in wheels)
    	{
    		
			wheel.wheelCollider.motorTorque = inputs.inputY * Acceleration *Time.deltaTime * 500;

    		if(wheel.axel == Axel.front)
    		{
    			wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, Steerangle * inputs.inputX, 0.1f);
    		}
    	}
			if(_rb.velocity.magnitude > maxSpeed)
			{
				_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);
			}
    }
    void animateWheels()
    {
    	foreach (var wheel in wheels)
    	{
    		Vector3 _pos = Vector3.zero;
    		Quaternion _rot = Quaternion.identity;
    		wheel.wheelCollider.GetWorldPose(out _pos, out _rot);
    		wheel.wheelTransform.position = _pos;
    		wheel.wheelTransform.rotation = _rot;
    	}
    }

    void Brake()
    {
    	
    		foreach(var wheel in wheels)
    		{
    			if(inputs.brakes)
    			{
    				wheel.wheelCollider.brakeTorque = BrakeForce * Acceleration;
					wheel.wheelCollider.motorTorque = 0;
    			}
    			else
    			{
    				wheel.wheelCollider.brakeTorque = 0;
					
    			}
    		}
    	
    }
	
}
