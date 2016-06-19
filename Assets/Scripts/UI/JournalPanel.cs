using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalPanel : MonoBehaviour {

	public static JournalPanel instance = null;

	public GameObject CluesContainer;
	public GameObject ClueDescription;
	public Text DescriptionTitle;
	public Text DescriptionText;
	public ClueItem ClueItem;

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

		ClueDescription.SetActive(false);
		RemoveNewLabels();
	}

	public void DisplayClue(Clue clue)
	{
		ClueItem clueItem = Instantiate(ClueItem, Vector3.zero, Quaternion.identity) as ClueItem;
		clueItem.transform.SetParent(CluesContainer.transform, false);
		clueItem.transform.localPosition = new Vector3(0, -60 - (40 * clue.Position), 0);

		clueItem.ClueName.text = clue.name;
		switch (clue.clueLevel)
		{
			case Clue.ClueLevel.firstLvl:
				clueItem.ClueType.color = Color.red;
				break;
			case Clue.ClueLevel.secondLvl:
				clueItem.ClueType.color = Color.yellow;
				break;
			case Clue.ClueLevel.thirdLvl:
				clueItem.ClueType.color = Color.green;
				break;
		}

		clueItem.name = clue.name;
		clueItem.ClueRef = clue;

		clueItem.NewLabel.gameObject.SetActive(clue.IsNew);

		Button b = clueItem.DisplayDescription;
		b.onClick.AddListener
		(
			() => { DisplayDescription(clueItem); }
		);
	}

	void DisplayDescription(ClueItem clueItem)
	{
		ClueDescription.SetActive(true);
		DescriptionTitle.text = clueItem.ClueRef.Name;
		DescriptionText.text = clueItem.ClueRef.Description;

		if (clueItem.NewLabel.gameObject.activeSelf)
		{
			clueItem.NewLabel.gameObject.SetActive(false);
		}
		
		clueItem.ClueRef.IsNew = false;
	}

	public void Testing()
	{
		Debug.Log(0);
	}

	private void RemoveNewLabels()
	{
		ClueItem[] tempClueItems = CluesContainer.GetComponentsInChildren<ClueItem>(true);
		foreach (ClueItem tempClueItem in tempClueItems)
		{
			if (tempClueItem.NewLabel.gameObject.activeSelf)
			{
				tempClueItem.NewLabel.gameObject.SetActive(false);
			}

			tempClueItem.ClueRef.IsNew = false;
		}
	}

}
