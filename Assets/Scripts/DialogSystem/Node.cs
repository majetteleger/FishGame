using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class Node : MonoBehaviour {

	public Condition[] conditions;
	public Option[] options;
	public bool permanentChoice;
	public Clue giveClue;
	public Item giveItem;
	public string LoadLevel;
	public Node nextNode;
	public bool singleRead;
	public Node altNode;

	private string _text;

	public bool HasBeenRead {
		get
		{
			return MainManager.instance.ActiveDialog.ReadNodes.Contains(Id);
		}
	}

	public int Id
	{
		get	{ return transform.GetSiblingIndex(); }
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
        newNode.name += ((newNode.transform.parent.childCount < 10) ? "0" : "") + (newNode.transform.parent.childCount-1);
    }
    
    void Start () {
		_text = GetComponent<Text> ().text;
	}
	
	public string getText(){
		return _text;
	}

}
