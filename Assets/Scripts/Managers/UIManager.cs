using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    public Canvas MainCanvas;
    public InGamePanel InGamePanel;
	public JournalPanel JournalPanel;
	public StartPanel StartPanel;
	public PausePanel PausePanel;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		LoadPanels();
	}

	void OnLevelWasLoaded()
	{
		LoadPanels();
	}

	private void LoadPanels()
	{
		if (InGamePanel == null)
			InGamePanel = GetComponentInChildren<InGamePanel>(true);

		if (JournalPanel == null)
			JournalPanel = GetComponentInChildren<JournalPanel>(true);

		if (StartPanel == null)
			StartPanel = GetComponentInChildren<StartPanel>(true);

		if (PausePanel == null)
			PausePanel = GetComponentInChildren<PausePanel>(true);
	}

}
