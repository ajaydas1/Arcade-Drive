using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider FR_Collider, FL_Collider, BR_Collider, BL_Collider;
    public Transform FR_Wheel, FL_Wheel, BR_Wheel, BL_Wheel;
    public float maxSteer = 30, maxSpeed = 2000;
    private Rigidbody _rigidbody;
    public Transform CenterOfMass;
    public float brakeForce = 500;

    void controls(){
        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");
        bool Brake = Input.GetKeyDown(KeyCode.Space);
        float accelarate = VerticalInput * maxSpeed;
        float turn = HorizontalInput * maxSteer;
        
        BR_Collider.motorTorque = accelarate;
        BL_Collider.motorTorque = accelarate;

        FL_Collider.steerAngle = turn;
        FR_Collider.steerAngle = turn;

        if(Brake){
            BR_Collider.brakeTorque = brakeForce;
            BL_Collider.brakeTorque = brakeForce;
            BR_Collider.motorTorque = 0;
            BL_Collider.motorTorque = 0;
        }
        else{
            BR_Collider.brakeTorque = 0;
            BL_Collider.brakeTorque = 0;
            BR_Collider.motorTorque = accelarate;
            BL_Collider.motorTorque = accelarate;
        }
    }
    void Update(){
        controls();
        WheelPoseChange(FR_Collider, FR_Wheel);
        WheelPoseChange(BR_Collider, BR_Wheel);
        WheelPoseChange(FL_Collider, FL_Wheel);
        WheelPoseChange(BL_Collider, BL_Wheel);
    }
    void FixedUpdate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = CenterOfMass.localPosition;
        

    }

    void WheelPoseChange(WheelCollider _Collider, Transform _Wheel){
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        _Collider.GetWorldPose(out pos, out rot);
        _Wheel.position = pos;
        _Wheel.rotation = rot * Quaternion.Euler(0,0,0);
    }

}
