using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearTrigger : MonoBehaviour
{
    public Level_Manager Manage;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Manage.EndGame();
            Time.timeScale = 0.5f;
        }
    }
}
