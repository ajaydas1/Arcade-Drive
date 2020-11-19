using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
	public void StartGame(){
		SceneManager.LoadScene("city");
	}
	public void QuitGame(){
		Application.Quit();
	}
}
