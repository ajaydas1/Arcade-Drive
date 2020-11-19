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
	[SerializeField]private float Acceleration = 100;
	[SerializeField]private float Steerangle = 35;
	[SerializeField]private float BrakeForce = 100;
	[SerializeField]private float inputX, inputY;
	[SerializeField]private List<Wheel> wheels;
	private Rigidbody _rb;
	[SerializeField]private Vector3 CenterOfMass;
	public bool brakes;
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
        Inputs();
        Move();
        animateWheels();
        Brake();
    }

    void Move()
    {
    	foreach (var wheel in wheels)
    	{
    		
			wheel.wheelCollider.motorTorque = inputY * Acceleration *Time.deltaTime * 500;

    		if(wheel.axel == Axel.front)
    		{
    			wheel.wheelCollider.steerAngle = inputX * Steerangle;
    		}
    	}
			if(_rb.velocity.magnitude > maxSpeed)
			{
				_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);
			}
    }

    void Inputs()
    {
    	inputY = Input.GetAxis("Vertical");
    	inputX = Input.GetAxis("Horizontal");
    	brakes = Input.GetKey(KeyCode.Space);

    }
	public void TouchMoveForward()
	{
		inputY = 1;
	}

	public void TouchMoveBackward()
	{
		inputY = -1;
	}

	public void TouchSteerRight()
	{
		inputX = 1;
	}
	public void TouchSteerLeft()
	{
		inputX = -1;
	}
	public void TouchMoveForwardDown()
	{
		inputY = 0;
	}

	public void TouchMoveBackwardDown()
	{
		inputY = 0;
	}

	public void TouchSteerRightDown()
	{
		inputX = 0;
	}
	public void TouchSteerLeftDown()
	{
		inputX = 0;
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
    			if(brakes)
    			{
    				wheel.wheelCollider.brakeTorque = BrakeForce * Acceleration;
    			}
    			else
    			{
    				wheel.wheelCollider.brakeTorque = 0;
    			}
    		}
    	
    }
}
