using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Condition : MonoBehaviour {

	public Clue[] Clues;
	public Item[] Items;
	public Node[] Nodes;
	public Node altNode;
	
    public bool isTrue(){

		if(Clues != null)
		{
			foreach (Clue clue in Clues)
			{
				if (clue.IsCollected == false)
				{
					return false;
				}
			}
		}
		
		if (Items != null)
		{
			foreach (Item item in Items)
			{
				if (item.IsCollected == false)
				{
					return false;
				}
			}
		}
		
		if (Nodes != null)
		{
			foreach (Node node in Nodes)
			{
				if (!node.HasBeenRead)
				{
					return false;
				}
			}
		}
		
		return true;
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
