using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class Dialog : MonoBehaviour {

	public string character;

	private Node _currentNode;
	private GameObject _dialogBox, _tempDialog, _optionButton, _tempOption;
	private Text[] _boxContents;
	private Node[] _dialogNodes;
	private Node _firstNode;

	public List<int> ReadNodes
	{
		get
		{
			if (!PlayerPrefs.HasKey(character + "ReadNodes") || PlayerPrefs.GetString(character + "ReadNodes") == "")
			{
				PlayerPrefs.SetString(character + "ReadNodes", "");
				return new List<int>();
			}

			string readNodesString = PlayerPrefs.GetString(character + "ReadNodes");
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
		newDialog.name += newDialog.transform.parent.childCount;
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
		
        if(_currentNode != null && _currentNode.options.Length > 0)
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
		else if (_currentNode.nextNode != null)
        {
            _currentNode = _currentNode.nextNode;
		}
        else
        {
			return endDialog();
        }

		if (_currentNode.conditions.Length > 0)
		{
			if (EvaluateConditions() != null)
			{
				_currentNode = EvaluateConditions().altNode;
			}
		}

		if (_currentNode.permanentChoice && _currentNode.nextNode != null)
		{
			_currentNode = _currentNode.nextNode;
		}

		if (_currentNode.singleRead && _currentNode.HasBeenRead && _currentNode.altNode != null)
		{
			_currentNode = _currentNode.altNode;
		}

		if (_currentNode.LoadLevel != null && _currentNode.LoadLevel != "")
		{
			SceneManager.LoadScene(_currentNode.LoadLevel);
			return endDialog();
		}
		
		DisplayNode();
		MarkNodeAsRead(_currentNode);

		if (_currentNode.giveClue != null) ClueManager.instance.GiveClue(_currentNode.giveClue);

		if (_currentNode.giveItem != null) ItemManager.instance.CollectItem(_currentNode.giveItem);
		
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
        if(_boxContents[0].text != character)
        {
            _boxContents[0].text = character;
        }
        _boxContents[1].text = _currentNode.getText();
        if (_currentNode.options.Length > 0)
        {
            for (int i = 0; i < _currentNode.options.Length; ++i)
            {
                _tempOption = Instantiate(_optionButton, new Vector3(65 + 115 * i, 40, 0), Quaternion.identity) as GameObject;
                _tempOption.GetComponentInChildren<Text>().text = _currentNode.options[i].getText();
                _tempOption.transform.SetParent(_tempDialog.transform);
                _tempOption.name = "Option" + (i + 1);
                Button b = _tempOption.GetComponent<Button>();
                int targetIndex = i;
                b.onClick.AddListener(() => setTargetNode(_currentNode.options[targetIndex].targetNode));
            }
            ClickManager.instance.CanClick = false;
        }

    }

    public void setTargetNode(Node targetNode)
    {
        _currentNode.nextNode = targetNode;
		ClickManager.instance.CanClick = true;
        advanceDialog();
    }

    public Condition EvaluateConditions()
    {
        for (int i = 0; i < _currentNode.conditions.Length; ++i)
        {
            if (_currentNode.conditions[i].isTrue())
            {
                return _currentNode.conditions[i];
            }
        }
        return null;
    }

	public void MarkNodeAsRead(Node node)
	{
		if (!ReadNodes.Contains(node.Id))
		{
			PlayerPrefs.SetString(character + "ReadNodes", PlayerPrefs.GetString(character + "ReadNodes") + node.Id.ToString() + ",");
		}
	}
	
	public void AssignCluesAndItems()
	{
		foreach (Node node in _dialogNodes)
		{
			// assigning giveClue clues
			if (node.giveClue != null)
			{
				foreach (Clue clue in ClueManager.instance.Clues)
				{
					if (node.giveClue.name == clue.name)
					{
						node.giveClue = clue;
					}
				}
			}

			// assigning giveItem items
			if (node.giveItem != null)
			{
				foreach (Item item in ItemManager.instance.Items)
				{
					if (node.giveItem.name == item.name)
					{
						node.giveItem = item;
					}
				}
			}

			// assigning conditions clues and items
			if (node.conditions.Length > 0)
			{
				foreach(Condition condition in node.conditions)
				{
					for (int i = 0; i < condition.Clues.Length; i++)
					{
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
						foreach (Item item in ItemManager.instance.Items)
						{
							if (condition.Items[i].name == item.name)
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

