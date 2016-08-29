using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuPanel : MonoBehaviour {

	public void UI_NewGame()
	{
		SaveGameManager.instance.ResetGame();
		SceneManager.LoadScene("Lake_outside");
	}

	public void UI_Resume()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("SavedScene"));
	}

	public void UI_Options()
	{

	}

	public void UI_Credits()
	{

	}

	public void UI_Exit()
	{

	}

}
