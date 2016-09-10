using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class Node : MonoBehaviour
{
	public string Text;
	public Node NextNode;
	public bool PermanentChoice;
	public Clue GiveClue;
	public Item GiveItem;
	public string LoadLevel;
	public bool SingleRead;
	public Node AltNode;
	
	public Condition[] Conditions
	{
		get { return GetComponentsInChildren<Condition>(); }
	}

	public Option[] Options
	{
		get	{ return GetComponentsInChildren<Option>();	}
	}

	public bool HasBeenRead
	{
		get	{ return MainManager.instance.ActiveDialog.ReadNodes.Contains(Id); }
	}

	public int Id
	{
		get { return transform.GetSiblingIndex(); }
	}
}
