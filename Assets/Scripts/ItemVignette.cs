using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemVignette : MonoBehaviour
{
	public Text ItemName;
	public Image NewLabel;
	public Button DisplayDescription;

	private Item _itemRef;

	public Item ItemRef
	{
		get { return _itemRef; }
		set { _itemRef = value; }
	}

}
