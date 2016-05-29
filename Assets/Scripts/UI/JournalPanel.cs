﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalPanel : MonoBehaviour {

	public static JournalPanel instance = null;

	public GameObject CluesContainer;
	public ClueItem ClueItem;

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);
	}
	
	void OnDisable()
	{
		RemoveNewLabels();
	}

	public void DisplayClue(Clue clue)
	{
		ClueItem clueItem = Instantiate(ClueItem, Vector3.zero, Quaternion.identity) as ClueItem;
		clueItem.transform.SetParent(CluesContainer.transform);
		clueItem.transform.localPosition = new Vector3(0, -35 * MainManager.instance.ClueManager.CluesFound, 0);

		clueItem.ClueName.text = clue.name;
		switch (clue.category)
		{
			case Clue.Category.firstLvl:
				clueItem.ClueType.color = Color.red;
				break;
			case Clue.Category.secondLvl:
				clueItem.ClueType.color = Color.yellow;
				break;
			case Clue.Category.thirdLvl:
				clueItem.ClueType.color = Color.green;
				break;
		}

		clueItem.name = clue.name;
	}

	private void RemoveNewLabels()
	{
		ClueItem[] tempClueItems = CluesContainer.GetComponentsInChildren<ClueItem>(true);
		foreach (ClueItem tempClueItem in tempClueItems)
		{
			if (tempClueItem.NewLabel.gameObject.activeSelf)
				tempClueItem.NewLabel.gameObject.SetActive(false);				
		}
	}

}