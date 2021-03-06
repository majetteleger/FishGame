﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;

[Serializable]
public class Dialog : MonoBehaviour {

	public string Title;

	private Node _currentNode;
	private GameObject _dialogBox, _tempDialog, _optionButton, _tempOption;
	private Text[] _boxContents;
	private Node[] _dialogNodes;
	private Node _firstNode;

	public List<int> ReadNodes
	{
		get
		{
			if (!PlayerPrefs.HasKey(Title + "ReadNodes") || PlayerPrefs.GetString(Title + "ReadNodes") == "")
			{
				PlayerPrefs.SetString(Title + "ReadNodes", "");
				return new List<int>();
			}

			string readNodesString = PlayerPrefs.GetString(Title + "ReadNodes");
			string[] readNodesStringArr = readNodesString.Remove(readNodesString.Length - 1).Split((new char[] { ',' }));
			int[] readNodesIntArr = Array.ConvertAll<string, int>(readNodesStringArr, int.Parse);
			List<int> readNodesList = readNodesIntArr.ToList();
			return readNodesList;
		}
	}

	[MenuItem("GameObject/FishGame/Dialog System/Dialog", false, 7)]
	static void CreateCustomGameObject(MenuCommand menuCommand)
	{
		GameObject newDialog = new GameObject("Dialog");
		GameObjectUtility.SetParentAndAlign(newDialog, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(newDialog, "Create " + newDialog.name);
		Selection.activeObject = newDialog;
		newDialog.AddComponent<Dialog>();
	}

	void Start() {
		_dialogNodes = GetComponentsInChildren<Node>();
		AssignCluesAndItems();

		_dialogBox = Resources.Load("DialogBox") as GameObject;
		_optionButton = Resources.Load("OptionButton") as GameObject;
	}
	
	public void initiateDialog(Node firstNode = null){
		
		MainManager.instance.ActiveDialog = this;
		
		_firstNode = firstNode == null ? transform.GetChild(0).GetComponent<Node>() : firstNode;

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = false;
		}

		_tempDialog = Instantiate (_dialogBox) as GameObject;
		_tempDialog.name = "DialogBox";

		advanceDialog();
	}

	public bool advanceDialog(){
		
        if(_currentNode != null && _currentNode.Options.Length > 0)
        {
            Button[] tmp = FindObjectsOfType<Button>();
            foreach (Button obj in tmp)
            {
                if (obj.transform.parent.name == "DialogBox")
                {
                    Destroy(obj.gameObject);
                }
            }
        }

		if (_currentNode == null)
		{
			_currentNode = _firstNode;
		}
		else if (_currentNode.NextNode != null)
        {
            _currentNode = _currentNode.NextNode;
		}
        else
        {
			return endDialog();
        }

		if (_currentNode.Conditions.Length > 0)
		{
			if (EvaluateConditions() != null)
			{
				_currentNode = EvaluateConditions().altNode;
			}
		}

		if (_currentNode.PermanentChoice && _currentNode.NextNode != null)
		{
			_currentNode = _currentNode.NextNode;
		}

		if (_currentNode.SingleRead && _currentNode.HasBeenRead && _currentNode.AltNode != null)
		{
			_currentNode = _currentNode.AltNode;
		}

		if (_currentNode.LoadLevel != null && _currentNode.LoadLevel != "")
		{
			SceneManager.LoadScene(_currentNode.LoadLevel);
			return endDialog();
		}
		
		DisplayNode();
		MarkNodeAsRead(_currentNode);

		if (_currentNode.GiveClue != null) ClueManager.instance.GiveClue(_currentNode.GiveClue);

		if (_currentNode.GiveItem != null) ItemManager.instance.CollectItem(_currentNode.GiveItem);
		
		return true;
	}

    public bool endDialog()
    {
        Destroy(_tempDialog);
		MainManager.instance.ActiveDialog = null;
		_currentNode = null;

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = true;
		}
		
		return true;
	}

    public void DisplayNode()
    {
        _boxContents = _tempDialog.GetComponentsInChildren<Text>();
        if(_boxContents[0].text != Title)
        {
            _boxContents[0].text = Title;
        }
        _boxContents[1].text = _currentNode.Text;
        if (_currentNode.Options.Length > 0)
        {
            for (int i = 0; i < _currentNode.Options.Length; ++i)
            {
                _tempOption = Instantiate(_optionButton, new Vector3(65 + 115 * i, 40, 0), Quaternion.identity) as GameObject;
                _tempOption.GetComponentInChildren<Text>().text = _currentNode.Options[i].Text;
                _tempOption.transform.SetParent(_tempDialog.transform);
                _tempOption.name = "Option" + (i + 1);
                Button b = _tempOption.GetComponent<Button>();
                int targetIndex = i;
                b.onClick.AddListener(() => setTargetNode(_currentNode.Options[targetIndex].TargetNode));
            }
            ClickManager.instance.CanClick = false;
        }

    }

    public void setTargetNode(Node targetNode)
    {
        _currentNode.NextNode = targetNode;
		ClickManager.instance.CanClick = true;
        advanceDialog();
    }

    public Condition EvaluateConditions()
    {
        for (int i = 0; i < _currentNode.Conditions.Length; ++i)
        {
            if (_currentNode.Conditions[i].isTrue())
            {
                return _currentNode.Conditions[i];
            }
        }
        return null;
    }

	public void MarkNodeAsRead(Node node)
	{
		if (!ReadNodes.Contains(node.Id))
		{
			PlayerPrefs.SetString(Title + "ReadNodes", PlayerPrefs.GetString(Title + "ReadNodes") + node.Id.ToString() + ",");
		}
	}
	
	public void AssignCluesAndItems()
	{
		foreach (Node node in _dialogNodes)
		{
			// assigning giveClue clues
			if (node.GiveClue != null)
			{
				foreach (Clue clue in ClueManager.instance.Clues)
				{
					if (node.GiveClue.name == clue.name)
					{
						node.GiveClue = clue;
					}
				}
			}

			// assigning giveItem items
			if (node.GiveItem != null)
			{
				foreach (Item item in ItemManager.instance.Items)
				{
					if (item != null && node.GiveItem.name == item.name)
					{
						node.GiveItem = item;
					}
				}
			}

			// assigning conditions clues and items
			if (node.Conditions.Length > 0)
			{
				foreach(Condition condition in node.Conditions)
				{
					for (int i = 0; i < condition.Clues.Length; i++)
					{
						if(condition.Clues[i] == null)
						{
							break;
						}
						foreach (Clue clue in ClueManager.instance.Clues)
						{
							if (condition.Clues[i].name == clue.name)
							{
								condition.Clues[i] = clue;
							}
						}
					}

					for (int i = 0; i < condition.Items.Length; i++)
					{
						if (condition.Items[i] == null)
						{
							break;
						}
						foreach (Item item in ItemManager.instance.Items)
						{
							if (item != null && condition.Items[i].name == item.name)
							{
								condition.Items[i] = item;
							}
						}
					}
				}
			}
		}
	}

}

