using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform Player;
    public Transform Camera;
    public float smoothAngle = 20f;
    void FixedUpdate()
    {
        
        Vector3 Smoothpos = Vector3.Lerp(transform.position, Camera.position, smoothAngle * Time.deltaTime);
        transform.LookAt(Player);
        transform.position = Smoothpos;
    }
}