using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGamePanel : MonoBehaviour {
	
	public Image NewClueLabel;
	
	public void ToggleJournal()
	{
		GameObject journalPanel = UIManager.instance.JournalPanel.gameObject;
		journalPanel.SetActive(!journalPanel.activeSelf);
		if (NewClueLabel.gameObject.activeSelf)
			NewClueLabel.gameObject.SetActive(false);
	}
	
}
