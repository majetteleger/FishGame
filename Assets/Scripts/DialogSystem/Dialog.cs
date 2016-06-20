using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Dialog : MonoBehaviour {

	public string character;
	
	private  Node _currentNode;
	private GameObject _dialogBox, _tempDialog, _optionButton, _tempOption;
	private Text[] _boxContents;
	private Node[] _dialogNodes;

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

    void Start(){
		_dialogNodes = GetComponentsInChildren<Node> ();
		AssignClues();

		_dialogBox = Resources.Load ("DialogBox") as GameObject;
		_optionButton = Resources.Load ("OptionButton") as GameObject;

	}
    
	public void initiateDialog(Node firstNode = null){
		MainManager.instance.ActiveDialog = this;
		if(firstNode == null)
		{
			firstNode = transform.GetChild(0).GetComponent<Node>();
		}

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = false;
		}
		
		_currentNode = firstNode;
        
		_tempDialog = Instantiate (_dialogBox) as GameObject;
		_tempDialog.name = "DialogBox";

        DisplayNode();

		if (_currentNode.giveClue != null)
		{
			ClueManager.instance.GiveClue(_currentNode.giveClue);
		}
	}

	public void advanceDialog(){

        if(_currentNode.options.Length > 0)
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
        
        if (_currentNode.nextNode != null)
        {
            
            _currentNode = _currentNode.nextNode;

            if (_currentNode.conditions.Length > 0)
            {
                if(EvaluateConditions() != null)
                {
                    _currentNode = EvaluateConditions().altNode;
                }
            }

            if(_currentNode.permanentChoice && _currentNode.nextNode != null)
            {
                _currentNode = _currentNode.nextNode;
            }

            DisplayNode();

            if (_currentNode.giveClue != null)
            {
                ClueManager.instance.GiveClue(_currentNode.giveClue);
            }

        }
        else
        {
            endDialog();
        }

	}

    public void endDialog()
    {
        Destroy(_tempDialog);
		MainManager.instance.ActiveDialog = null;

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = true;
		}
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
	
	public void AssignClues()
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

			// assigning conditions clues
			if(node.conditions.Length > 0)
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
				}
			}
		}
	}

}

