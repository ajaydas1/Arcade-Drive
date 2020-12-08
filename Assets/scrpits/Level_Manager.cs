using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_Manager : MonoBehaviour
{
    [SerializeField]private GameObject[] CarList;
    public Transform StartPosition;
    public GameObject FinishGameUI;
    public bool isLevelComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        int SelectedCar = PlayerPrefs.GetInt("SelectedCar");
        GameObject prefab = CarList[SelectedCar];
        GameObject clone = Instantiate(prefab, StartPosition.position, Quaternion.identity);
    }

    void Update()
    {
        if(isLevelComplete == false)
        {
            FinishGameUI.SetActive(false);
        }else FinishGameUI.SetActive(true);
    }

    public void EndGame()
    {
        Debug.Log("Level  Clear");
        isLevelComplete = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public void Quit(){
		Application.Quit();
	}

	public void mainMenu(){
		SceneManager.LoadScene("mainmenu");
	}

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
