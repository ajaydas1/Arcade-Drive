using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{

	[SerializeField]private GameObject[] CarList;
	public int SelectedCar = 0;
	void Start()
	{
		for(int i = 0; i < CarList.Length; i++)
		{
			if(i == 0) CarList[i].SetActive(true);
			else CarList[i].SetActive(false);
		}
	}
	public void StartGame(){
		PlayerPrefs.SetInt("SelectedCar",SelectedCar);
		SceneManager.LoadScene(1);
	}
	public void QuitGame(){
		Application.Quit();
	}

	public void NextCar()
	{
		CarList[SelectedCar].SetActive(false);
		SelectedCar = (SelectedCar + 1) % CarList.Length;
		CarList[SelectedCar].SetActive(true);
	}

	public void PreviousCar()
	{
		CarList[SelectedCar].SetActive(false);
		SelectedCar--;
		if(SelectedCar < 0) SelectedCar += CarList.Length;
		CarList[SelectedCar].SetActive(true);
	}
	void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow)) PreviousCar();
		if(Input.GetKeyDown(KeyCode.RightArrow)) NextCar();
	}

}
