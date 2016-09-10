using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEditor;

[Serializable]
public class Item : MonoBehaviour {
	
	public string Name;
	public Sprite Sprite;
	public string Description;
	public bool DestroyInGameItemOnCollection;

	public bool IsCollected
	{
		get { return ItemManager.instance.CollectedItems.Contains(this); }
	}

	private bool _isNew;
	public bool IsNew
	{
		get { return _isNew; }
		set { _isNew = value; }
	}
	
	[MenuItem("GameObject/FishGame/Item", false, 7)]
	static void CreateCustomGameObject(MenuCommand menuCommand)
	{
		GameObject newItem = new GameObject("Item");
		GameObjectUtility.SetParentAndAlign(newItem, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(newItem, "Create " + newItem.name);
		Selection.activeObject = newItem;
		newItem.AddComponent<Item>();
	}
}
