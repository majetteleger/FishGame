using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(Node)), CanEditMultipleObjects]
public class NodeEditor : Editor
{
	private Node _node;

	void OnEnable()
	{
		_node = target as Node;
		EditorStyles.textArea.wordWrap = true;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Text", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_node.Text = EditorGUILayout.TextArea(_node.Text);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
		EditorGUILayout.LabelField("Next Node", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_node.NextNode = (Node)EditorGUILayout.ObjectField(_node.NextNode, typeof(Node), true);
		EditorGUILayout.EndHorizontal();

		if (_node.Options.Length > 0)
		{
			EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
			EditorGUILayout.LabelField("Permanent Choice", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
			_node.PermanentChoice = EditorGUILayout.Toggle(_node.PermanentChoice);
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
		EditorGUILayout.LabelField("Give Clue", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_node.GiveClue = (Clue)EditorGUILayout.ObjectField(_node.GiveClue, typeof(Clue), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
		EditorGUILayout.LabelField("Give Item", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_node.GiveItem = (Item)EditorGUILayout.ObjectField(_node.GiveItem, typeof(Item), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
		EditorGUILayout.LabelField("Load Level", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_node.LoadLevel = EditorGUILayout.TextField(_node.LoadLevel);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
		EditorGUILayout.LabelField("Single Read", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_node.SingleRead = EditorGUILayout.Toggle(_node.SingleRead);
		EditorGUILayout.EndHorizontal();

		if (_node.SingleRead)
		{
			EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
			EditorGUILayout.LabelField("Alt Node", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
			_node.AltNode = (Node)EditorGUILayout.ObjectField(_node.AltNode, typeof(Node), true);
			EditorGUILayout.EndHorizontal();
		}

		if (GUILayout.Button("Add Option"))
		{
			GameObject optionsFolder = _node.Options.Length != 0 ? _node.transform.FindChild("Options").gameObject : new GameObject("Options");

			if (_node.Options.Length == 0)
			{
				optionsFolder.transform.SetParent(_node.transform);
			}

			GameObject newOption = new GameObject("<option>");
			Undo.RegisterCreatedObjectUndo(newOption, "Create " + newOption.name);
			newOption.AddComponent<Option>();
			newOption.transform.SetParent(optionsFolder.transform);
		}

		if (GUILayout.Button("Add Condition"))
		{
			GameObject conditionsFolder = _node.Conditions.Length != 0 ? _node.transform.FindChild("Conditions").gameObject : new GameObject("Conditions");

			if(_node.Conditions.Length == 0)
			{
				conditionsFolder.transform.SetParent(_node.transform);
			}
			
			GameObject newCondition = new GameObject("<condition>");
			Undo.RegisterCreatedObjectUndo(newCondition, "Create " + newCondition.name);
			newCondition.AddComponent<Condition>();
			newCondition.transform.SetParent(conditionsFolder.transform);
		}

		DisplayName();
	}

	private void DisplayName()
	{
		_node.gameObject.name = (_node.Text == "" || _node.Text == null) ? "<node>" : "\"" + _node.Text.Substring(0, _node.Text.Length > 30 ? 30 : _node.Text.Length) + (_node.Text.Length > 30 ? "..." : "") + "\"";
	}
}
