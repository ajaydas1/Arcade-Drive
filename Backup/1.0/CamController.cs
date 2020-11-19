using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject Player;
    public float smoothangles = 10f;
    void FixedUpdate()
    {
        Vector3 smoothPos = Vector3.Lerp(transform.position ,Player.transform.position, smoothangles * Time.deltaTime);
        transform.position = smoothPos;
        transform.rotation = Player.transform.rotation;

    }
}
