using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

	public InGamePanel InGamePanel;
	public JournalPanel JournalPanel;
	public InventoryPanel InventoryPanel;
	public StartPanel StartPanel;
	public PausePanel PausePanel;

	public InGamePanel InGamePanelPrefab;
	public JournalPanel JournalPanelPrefab;
	public InventoryPanel InventoryPanelPrefab;
	public StartPanel StartPanelPrefab;
	public PausePanel PausePanelPrefab;

	private Canvas _mainCanvas;

	public Canvas MainCanvas
	{
		get { return _mainCanvas; }
		set { _mainCanvas = value; }
	}

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
		if(_mainCanvas == null)
		{
			_mainCanvas = FindObjectOfType<Canvas>();
		}

		if(!FindObjectOfType<InGamePanel>())
		{
			InGamePanel = Instantiate(InGamePanelPrefab);
			InGamePanel.transform.SetParent(_mainCanvas.transform, false);
		}

		if (!FindObjectOfType<JournalPanel>())
		{
			JournalPanel = Instantiate(JournalPanelPrefab);
			JournalPanel.transform.SetParent(_mainCanvas.transform, false);
			JournalPanel.gameObject.SetActive(false);

			foreach (Clue clue in ClueManager.instance.Clues)
			{
				if(clue.State)
				{
					JournalPanel.instance.DisplayClue(clue);
				}
			}
		}

		if (!FindObjectOfType<InventoryPanel>())
		{
			InventoryPanel = Instantiate(InventoryPanelPrefab);
			InventoryPanel.transform.SetParent(_mainCanvas.transform, false);
			InventoryPanel.gameObject.SetActive(false);

			foreach (Item item in ItemManager.instance.Items)
			{
				if (item.IsCollected)
				{
					InventoryPanel.instance.DisplayItem(item);
				}
			}
		}

		if (!FindObjectOfType<StartPanel>())
		{
			StartPanel = Instantiate(StartPanelPrefab);
			StartPanel.transform.SetParent(_mainCanvas.transform, false);
			StartPanel.gameObject.SetActive(false);
		}

		if (!FindObjectOfType<PausePanel>())
		{
			PausePanel = Instantiate(PausePanelPrefab);
			PausePanel.transform.SetParent(_mainCanvas.transform, false);
			PausePanel.gameObject.SetActive(false);
		}
	}

}
