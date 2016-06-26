using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{

	public static ItemManager instance = null;

	public Item[] Items;

	//private Clue[] _clues;

	//public Clue[] Clues
	//{
	//	get { return _clues; }
	//	set { _clues = value; }
	//}

	private static int _itemsFound = 0;

	public int ItemsFound
	{
		get { return _itemsFound; }
		set { _itemsFound = value; }
	}

	private void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		Items = FindObjectsOfType<Item>();
	}


	public void CollectItem(Item item)
	{
		if (item.IsCollected == false)
		{
			item.IsCollected = true;
			item.IsNew = true;
			item.Position = _itemsFound;
			InventoryPanel.instance.DisplayItem(item);
			UIManager.instance.InGamePanel.NewItemLabel.gameObject.SetActive(true);
			_itemsFound++;
		}
	}

	public bool CheckItem(Item item)
	{
		return item.IsCollected;
	}

}
