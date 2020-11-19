using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform Player;
    public Transform Camera;
    void FixedUpdate()
    {
        transform.LookAt(Player);
        transform.position = Camera.position;
    }
}
