using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClueManager : MonoBehaviour {

    public static ClueManager instance = null;

    public Clue[] Clues;

	//private Clue[] _clues;

	//public Clue[] Clues
	//{
	//	get { return _clues; }
	//	set { _clues = value; }
	//}

	private static int _cluesFound = 0;

	public int CluesFound
	{
		get { return _cluesFound; }
		set { _cluesFound = value; }
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
		if(clue.State == false)
		{
			clue.State = true;
			clue.IsNew = true;
			clue.Position = _cluesFound;
			JournalPanel.instance.DisplayClue(clue);
			UIManager.instance.InGamePanel.NewClueLabel.gameObject.SetActive(true);
			_cluesFound++;
		}
    }

    public bool CheckClue(Clue clue)
    {
        return clue.State;
    }

}
