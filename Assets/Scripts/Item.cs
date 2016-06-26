using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class Item : MonoBehaviour {
	
	public string Name;
	public Sprite Sprite;
	public string Description;
	public bool DestroyInGameItemOnCollection;

	private static int _itemsFound = 0;
	public static int ItemsFound
	{
		get { return _itemsFound; }
	}

	private bool _isCollected;
	public bool IsCollected
	{
		get { return _isCollected; }
		set { _isCollected = value; }
	}
	
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
