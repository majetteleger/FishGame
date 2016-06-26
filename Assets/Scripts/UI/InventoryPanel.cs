using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryPanel : MonoBehaviour {

	public static InventoryPanel instance = null;

	public GameObject ItemContainer;
	public GameObject ItemDescription;
	public Text DescriptionTitle;
	public Text DescriptionText;
	public ItemVignette ItemVignettePrefab;

	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);
	}

	void OnEnable()
	{
		ClickManager.instance.CanClick = false;

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = false;
		}
	}

	void OnDisable()
	{
		ClickManager.instance.CanClick = true;

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = true;
		}

		ItemDescription.SetActive(false);
		RemoveNewLabels();
	}

	public void DisplayItem(Item item)
	{
		ItemVignette itemVignette = Instantiate(ItemVignettePrefab) as ItemVignette;
		itemVignette.transform.SetParent(ItemContainer.transform, false);

		itemVignette.ItemName.text = item.Name;
		itemVignette.GetComponent<Image>().sprite = item.Sprite;

		itemVignette.name = item.Name;
		itemVignette.ItemRef = item;

		itemVignette.NewLabel.gameObject.SetActive(item.IsNew);

		Button b = itemVignette.DisplayDescription;
		b.onClick.AddListener
		(
			() => { DisplayDescription(itemVignette); }
		);
	}

	private void DisplayDescription(ItemVignette itemVignette)
	{
		ItemDescription.SetActive(true);
		DescriptionTitle.text = itemVignette.ItemRef.Name;
		DescriptionText.text = itemVignette.ItemRef.Description;

		if (itemVignette.NewLabel.gameObject.activeSelf)
		{
			itemVignette.NewLabel.gameObject.SetActive(false);
		}

		itemVignette.ItemRef.IsNew = false;
	}

	private void RemoveNewLabels()
	{
		ItemVignette[] tempItemVignettes = ItemContainer.GetComponentsInChildren<ItemVignette>(true);
		foreach (ItemVignette tempItemVignette in tempItemVignettes)
		{
			if (tempItemVignette.NewLabel.gameObject.activeSelf)
			{
				tempItemVignette.NewLabel.gameObject.SetActive(false);
			}

			tempItemVignette.ItemRef.IsNew = false;
		}
	}
}
