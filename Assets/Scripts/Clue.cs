using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class Clue : MonoBehaviour {
	
	// public stuff
	public bool State;
	public string Name;
	public string Description;
	public enum ClueLevel
	{
		firstLvl,
		secondLvl,
		thirdLvl
	}
	public ClueLevel clueLevel;
	//

	private bool _isNew;
	public bool IsNew
	{
		get { return _isNew; }
		set { _isNew = value; }
	}

	private int _position;
	public int Position
	{
		get { return _position; }
		set { _position = value; }
	}
	//

	// static stuff
	public static int cluesFound = 0;
	public static int getcluesFound(){return cluesFound;}
	//

	[MenuItem("GameObject/FishGame/Clue", false, 7)]
	static void CreateCustomGameObject(MenuCommand menuCommand)
	{
		GameObject newClue = new GameObject("Clue");
		GameObjectUtility.SetParentAndAlign(newClue, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(newClue, "Create " + newClue.name);
		Selection.activeObject = newClue;
		newClue.AddComponent<Clue>();
	}
}
