using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClueManager : MonoBehaviour {

    public static ClueManager instance = null;

    public Clue[] Clues;

	public List<Clue> CollectedClues
	{
		get
		{
			if (!PlayerPrefs.HasKey("CollectedClues") || PlayerPrefs.GetString("CollectedClues") == "")
			{
				PlayerPrefs.SetString("CollectedClues", "");
				return new List<Clue>();
			}

			string collectedCluesString = PlayerPrefs.GetString("CollectedClues");
			string[] collectedCluesStringArr = collectedCluesString.Remove(collectedCluesString.Length - 1).Split((new char[] { ',' }));

			List<Clue> collectedCluesList = new List<Clue>();
			foreach(string str in collectedCluesStringArr)
			{
				foreach (Clue clue in Clues)
				{
					if(str == clue.name)
					{
						collectedCluesList.Add(clue);
					}
				}
			}

			return collectedCluesList;
		}
	}
	
	private void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		Clues = FindObjectsOfType<Clue>();
	}


	public void GiveClue(Clue clue)
    {
		if(clue.IsCollected == false)
		{
			PlayerPrefs.SetString("CollectedClues", PlayerPrefs.GetString("CollectedClues") + clue.name + ",");

			clue.IsNew = true;
			JournalPanel.instance.DisplayClue(clue);
			UIManager.instance.InGamePanel.NewClueLabel.gameObject.SetActive(true);
		}
    }

    public bool CheckClue(Clue clue)
    {
        return clue.IsCollected;
    }

}
