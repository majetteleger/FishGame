using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalPanel : MonoBehaviour {

	public static JournalPanel instance = null;

	public GameObject CluesContainer;
	public GameObject ClueDescription;
	public Text DescriptionTitle;
	public Text DescriptionText;
	public ClueVignette ClueVignettePrefab;

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
		ClueVignette clueVignette = Instantiate(ClueVignettePrefab) as ClueVignette;
		clueVignette.transform.SetParent(CluesContainer.transform, false);

		clueVignette.ClueName.text = clue.Name;
		switch (clue.clueLevel)
		{
			case Clue.ClueLevel.firstLvl:
				clueVignette.ClueType.color = Color.red;
				break;
			case Clue.ClueLevel.secondLvl:
				clueVignette.ClueType.color = Color.yellow;
				break;
			case Clue.ClueLevel.thirdLvl:
				clueVignette.ClueType.color = Color.green;
				break;
		}

		clueVignette.name = clue.Name;
		clueVignette.ClueRef = clue;

		clueVignette.NewLabel.gameObject.SetActive(clue.IsNew);

		Button b = clueVignette.DisplayDescription;
		b.onClick.AddListener
		(
			() => { DisplayDescription(clueVignette); }
		);
	}

	private void DisplayDescription(ClueVignette clueVignette)
	{
		ClueDescription.SetActive(true);
		DescriptionTitle.text = clueVignette.ClueRef.Name;
		DescriptionText.text = clueVignette.ClueRef.Description;

		if (clueVignette.NewLabel.gameObject.activeSelf)
		{
			clueVignette.NewLabel.gameObject.SetActive(false);
		}

		clueVignette.ClueRef.IsNew = false;
	}
	
	private void RemoveNewLabels()
	{
		ClueVignette[] tempClueItems = CluesContainer.GetComponentsInChildren<ClueVignette>(true);
		foreach (ClueVignette tempClueItem in tempClueItems)
		{
			if (tempClueItem.NewLabel.gameObject.activeSelf)
			{
				tempClueItem.NewLabel.gameObject.SetActive(false);
			}

			tempClueItem.ClueRef.IsNew = false;
		}
	}

}
