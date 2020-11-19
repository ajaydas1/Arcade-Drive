using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class lose : MonoBehaviour
{
    bool gamePause = true;
    public GameObject PauseMenu;
    void OnTriggerEnter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Update()
    {
        if(transform.position.y < - 30)
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePause)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    

    }

    public void Play()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
