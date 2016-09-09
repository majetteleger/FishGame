using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class Node : MonoBehaviour
{
	[Multiline]
	public string Text;
	public Node NextNode;
	public bool PermanentChoice;
	public Clue GiveClue;
	public Item GiveItem;
	public string LoadLevel;
	public bool SingleRead;
	public Node AltNode;
	
	public Condition[] Conditions
	{
		get { return GetComponentsInChildren<Condition>(); }
	}

	public Option[] Options
	{
		get	{ return GetComponentsInChildren<Option>();	}
	}

	public bool HasBeenRead
	{
		get	{ return MainManager.instance.ActiveDialog.ReadNodes.Contains(Id); }
	}

	public int Id
	{
		get { return transform.GetSiblingIndex(); }
	}

	[MenuItem("GameObject/FishGame/Dialog System/Node", false, 8)]
	static void CreateCustomGameObject(MenuCommand menuCommand)
	{
		GameObject newNode = new GameObject("Node");
		GameObjectUtility.SetParentAndAlign(newNode, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(newNode, "Create " + newNode.name);
		Selection.activeObject = newNode;
		newNode.AddComponent<Node>();
		newNode.AddComponent<Text>();
	}
}
