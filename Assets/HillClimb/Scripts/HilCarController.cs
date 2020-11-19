using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HilCarController : MonoBehaviour
{
    //public WheelCollider[] HCWheelColliders;
    public List<WheelCollider> HCWheelColliders;
    public List<Transform> HCWheels;
    public float movement;
    public float HCAcceleration = 500f;
    float HCmaxSpeed = 50f;
    public Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        animateWheel();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
    	foreach (var wheel in HCWheelColliders)
    	{
            wheel.motorTorque = HCAcceleration * -movement;
            if (_rb.velocity.z > HCmaxSpeed || _rb.velocity.z < -HCmaxSpeed)
            {
                wheel.motorTorque = 0;
            }
        }


    }
    void animateWheel()
    {
    	for(int i = 0; i < 4  ;i++)
    	{
    		Vector3 _pos = Vector3.zero;
    		Quaternion _rot = Quaternion.identity;
    		HCWheelColliders[i].GetWorldPose(out _pos, out _rot);
            HCWheels[i].transform.position = _pos;
            HCWheels[i].transform.rotation = _rot;

    	}
    }

    void onTriggerEnter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
