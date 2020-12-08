using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float inputX;
	public float inputY;
	public bool brakes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Inputs()
    {
    	inputY = Input.GetAxis("Vertical");
    	inputX = Input.GetAxis("Horizontal");
    	brakes = Input.GetKey(KeyCode.Space);
    }	

    public void ForwardPressed()
    {
        inputY = 1;
    }

    public void BackwardPressed()
    {
        inputY = -1;
    }

    public void RightPressed()
    {
        inputX = 1;
    }

    public void LeftPressed()
    {
        inputX = -1;
    }

    public void NotMove()
    {
        inputY = 0;
    }

    public void NotSteer()
    {
        inputX = 0;
    }
}
