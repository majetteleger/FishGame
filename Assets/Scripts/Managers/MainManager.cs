using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainManager : MonoBehaviour {

	public static MainManager instance = null;

	public ClueManager ClueManager;
	public ItemManager ItemManager;
	public ClickManager ClickManager;
	public UIManager UIManager;
	public PlayerController PlayerPrefab;

	private PlayerController _playerController;

	public PlayerController PlayerController
	{
		get { return _playerController; }
		set { _playerController = value; }
	}

	private Dialog _activeDialog;

	public Dialog ActiveDialog
	{
		get { return _activeDialog; }
		set { _activeDialog = value; }
	}
	
	void Awake()
	{
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if(FindObjectOfType<ClueManager>() == null)
		{
			Instantiate(ClueManager);
		}

		if (FindObjectOfType<ItemManager>() == null)
		{
			Instantiate(ItemManager);
		}

		if (FindObjectOfType<ClickManager>() == null)
		{
			Instantiate(ClickManager);
		}

		if (FindObjectOfType<UIManager>() == null)
		{
			Instantiate(UIManager);
		}

	}

	void Update()
	{
		if (FindObjectOfType<PlayerController>() == null && Camera.main.GetComponent<iTweenPath>())
		{
			_playerController = Instantiate(PlayerPrefab) as PlayerController;
		}
		//else if(FindObjectOfType<PlayerController>() != null && !Camera.main.GetComponent<iTweenPath>())
		//{
		//	Debug.Log(1);
		//	_playerController = null;
		//}
	}
    
}
