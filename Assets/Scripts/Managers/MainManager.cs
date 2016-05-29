using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainManager : MonoBehaviour {

	public static MainManager instance = null;

	public ClueManager ClueManager;
	public ClickManager ClickManager;
	public UIManager UIManager;

	private void Awake()
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

		if (FindObjectOfType<ClickManager>() == null)
		{
			Instantiate(ClickManager);
		}

		if (FindObjectOfType<UIManager>() == null)
		{
			Instantiate(UIManager);
		}

	}
    
}
