using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Condition : MonoBehaviour {

	public Clue[] Clues;
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
		foreach (Clue clue in Clues)
		{
			if(clue.State == false){
				return false;
			}
		} 
		return true;
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
