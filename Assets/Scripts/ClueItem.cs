using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClueItem: MonoBehaviour {

	public Text ClueName;
	public Image ClueType;
	public Image NewLabel;
	public Button DisplayDescription;

	private Clue _clueRef;

	public Clue ClueRef
	{
		get { return _clueRef; }
		set { _clueRef = value; }
	}
	
}
