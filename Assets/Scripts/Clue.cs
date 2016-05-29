using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class Clue : MonoBehaviour {
	
	// public stuff
	public bool state;
	public string name;
	public string description;
	public enum ClueLevel
	{
		firstLvl,
		secondLvl,
		thirdLvl
	}
	public ClueLevel clueLevel;
	//

	// private stuff
	public int position;
	private enum Status
	{
		newC,
		normalC,
		usedC,
	}
	private Status status;
	//

	// static stuff
	public static int cluesFound = 0;
	public static int getcluesFound(){return cluesFound;}
	//

	// accessors
	public bool getState(){return state;}
	public string getName(){return name;}
	public int getPosition(){return position;}
	//

	// mutators
	public void setState(bool s){state = s;}
	public void setPosition(int p){position = p;}
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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTrue(){
		
	}

}
