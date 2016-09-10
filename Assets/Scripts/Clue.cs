using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEditor;

[Serializable]
public class Clue : MonoBehaviour {
	
	public string Name;
	public string Description;
	public enum ClueLevel
	{
		firstLvl,
		secondLvl,
		thirdLvl
	}
	public ClueLevel clueLevel;

	public bool IsCollected
	{
		get { return ClueManager.instance.CollectedClues.Contains(this); }
	}

	private bool _isNew;

	public bool IsNew
	{
		get { return _isNew; }
		set { _isNew = value; }
	}
	
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
