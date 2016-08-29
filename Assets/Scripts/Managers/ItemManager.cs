using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
	public static ItemManager instance = null;

	public Item[] Items;

	public List<Item> CollectedItems
	{
		get
		{
			if (!PlayerPrefs.HasKey("CollectedItems") || PlayerPrefs.GetString("CollectedItems") == "")
			{
				PlayerPrefs.SetString("CollectedItems", "");
				return new List<Item>();
			}

			string collectedItemsString = PlayerPrefs.GetString("CollectedItems");
			string[] collectedItemsStringArr = collectedItemsString.Remove(collectedItemsString.Length - 1).Split((new char[] { ',' }));

			List<Item> collectedItemsList = new List<Item>();
			foreach (string str in collectedItemsStringArr)
			{
				foreach (Item item in Items)
				{
					if (str == item.name)
					{
						collectedItemsList.Add(item);
					}
				}
			}

			return collectedItemsList;
		}
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
			PlayerPrefs.SetString("CollectedItems", PlayerPrefs.GetString("CollectedItems") + item.name + ",");

			item.IsNew = true;
			InventoryPanel.instance.DisplayItem(item);
			UIManager.instance.InGamePanel.NewItemLabel.gameObject.SetActive(true);
		}
	}

	public bool CheckItem(Item item)
	{
		return item.IsCollected;
	}

}
