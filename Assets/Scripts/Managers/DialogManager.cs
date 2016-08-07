using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
	public static DialogManager instance = null;

	public Dialog[] Dialogs;
	
	private static int _itemsFound = 0;
	
	private void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		Dialogs = FindObjectsOfType<Dialog>();
	}
}
