using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Condition : MonoBehaviour {

	public Clue[] Clues;
	public Item[] Items;
	public Node[] Nodes;
	public Node altNode;

    [MenuItem("GameObject/FishGame/Dialog System/Condition", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject newCondition = new GameObject("Condition");
        GameObjectUtility.SetParentAndAlign(newCondition, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(newCondition, "Create " + newCondition.name);
        Selection.activeObject = newCondition;
        newCondition.AddComponent<Condition>();
        newCondition.name += ((newCondition.transform.parent.childCount < 10) ? "0" : "") + (newCondition.transform.parent.childCount - 1);
    }

    public bool isTrue(){

		if(Clues != null)
		{
			foreach (Clue clue in Clues)
			{
				if (clue.IsCollected == false)
				{
					return false;
				}
			}
		}
		
		if (Items != null)
		{
			foreach (Item item in Items)
			{
				if (item.IsCollected == false)
				{
					return false;
				}
			}
		}
		
		if (Nodes != null)
		{
			foreach (Node node in Nodes)
			{
				if (!node.HasBeenRead)
				{
					return false;
				}
			}
		}
		
		return true;
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
