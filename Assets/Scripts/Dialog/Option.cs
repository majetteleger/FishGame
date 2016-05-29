using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Option : MonoBehaviour {

	public Node targetNode;

	string text;

    [MenuItem("GameObject/Dialog/Option", false, 9)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject newOption = new GameObject("Option");
        GameObjectUtility.SetParentAndAlign(newOption, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(newOption, "Create " + newOption.name);
        Selection.activeObject = newOption;
        newOption.AddComponent<Option>();
        newOption.AddComponent<Text>();
        newOption.name += ((newOption.transform.parent.childCount < 10) ? "0" : "") + (newOption.transform.parent.childCount - 1);
    }

    void Start () {
		text = GetComponent<Text> ().text;
	}
	
	void Update () {
	
	}

	public string getText(){
		return text;
	}

}
