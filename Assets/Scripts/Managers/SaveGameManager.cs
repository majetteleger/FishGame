using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class SaveGameManager : MonoBehaviour {

	public static SaveGameManager instance = null;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void ResetGame()
	{
		PlayerPrefs.DeleteAll();
	}

	public void SaveGame()
	{
		PlayerPrefs.SetString("SavedScene", EditorSceneManager.GetActiveScene().name);
	}
}
