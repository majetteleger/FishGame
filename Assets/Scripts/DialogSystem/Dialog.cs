using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Dialog : MonoBehaviour {

	public string character;
    public Node firstNode;

    Node currentNode;
	GameObject dialogBox, tempDialog, optionButton, tempOption;
	Text[] boxContents;

	Node[] dialogNodes;

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
		dialogNodes = GetComponentsInChildren<Node> ();
		AssignClues();

		dialogBox = Resources.Load ("DialogBox") as GameObject;
		optionButton = Resources.Load ("OptionButton") as GameObject;

	}
    
	public void initiateDialog(){
		MainManager.instance.ActiveDialog = this;

		if(MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = false;
		}
		
		currentNode = firstNode;
        
		tempDialog = Instantiate (dialogBox) as GameObject;
		tempDialog.name = "DialogBox";

        DisplayNode();

		if (currentNode.giveClue != null)
		{
			ClueManager.instance.GiveClue(currentNode.giveClue);
		}
	}

	public void advanceDialog(){

        if(currentNode.options.Length > 0)
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
        
        if (currentNode.nextNode != null)
        {
            
            currentNode = currentNode.nextNode;

            if (currentNode.conditions.Length > 0)
            {
                if(EvaluateConditions() != null)
                {
                    currentNode = EvaluateConditions().altNode;
                }
            }

            if(currentNode.permanentChoice && currentNode.nextNode != null)
            {
                currentNode = currentNode.nextNode;
            }

            DisplayNode();

            if (currentNode.giveClue != null)
            {
                ClueManager.instance.GiveClue(currentNode.giveClue);
            }

        }
        else
        {
            endDialog();
        }

	}

    public void endDialog()
    {
        Destroy(tempDialog);
		MainManager.instance.ActiveDialog = null;

		if (MainManager.instance.PlayerController != null)
		{
			MainManager.instance.PlayerController.CanMove = true;
		}
	}

    public void DisplayNode()
    {
        boxContents = tempDialog.GetComponentsInChildren<Text>();
        if(boxContents[0].text != character)
        {
            boxContents[0].text = character;
        }
        boxContents[1].text = currentNode.getText();
        if (currentNode.options.Length > 0)
        {
            for (int i = 0; i < currentNode.options.Length; ++i)
            {
                tempOption = Instantiate(optionButton, new Vector3(65 + 115 * i, 40, 0), Quaternion.identity) as GameObject;
                tempOption.GetComponentInChildren<Text>().text = currentNode.options[i].getText();
                tempOption.transform.SetParent(tempDialog.transform);
                tempOption.name = "Option" + (i + 1);
                Button b = tempOption.GetComponent<Button>();
                int targetIndex = i;
                b.onClick.AddListener(() => setTargetNode(currentNode.options[targetIndex].targetNode));
            }
            ClickManager.instance.CanClick = false;
        }

    }

    public void setTargetNode(Node targetNode)
    {
        currentNode.nextNode = targetNode;
		ClickManager.instance.CanClick = true;
        advanceDialog();
    }

    public Condition EvaluateConditions()
    {
        for (int i = 0; i < currentNode.conditions.Length; ++i)
        {
            if (currentNode.conditions[i].isTrue())
            {
                return currentNode.conditions[i];
            }
        }
        return null;
    }
	
	public void AssignClues()
	{
		foreach (Node node in dialogNodes)
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

