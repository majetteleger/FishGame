using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGamePanel : MonoBehaviour {

	public static InGamePanel instance = null;

	public Image NewClueLabel;
	public Image NewItemLabel;
	public Button ObjectActionButton;

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);
	}

	public void ToggleJournal()
	{
		GameObject journalPanel = UIManager.instance.JournalPanel.gameObject;
		journalPanel.SetActive(!journalPanel.activeSelf);
		if (NewClueLabel.gameObject.activeSelf)
			NewClueLabel.gameObject.SetActive(false);
	}

	public void ToggleInventory()
	{
		GameObject InventoryPanel = UIManager.instance.InventoryPanel.gameObject;
		InventoryPanel.SetActive(!InventoryPanel.activeSelf);
		if (NewItemLabel.gameObject.activeSelf)
			NewItemLabel.gameObject.SetActive(false);
	}

}
