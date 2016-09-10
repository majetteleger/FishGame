using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Condition)), CanEditMultipleObjects]
public class ConditionEditor : Editor
{
	private Condition _condition;

	void OnEnable()
	{
		_condition = target as Condition;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		DisplayName();
	}

	private void DisplayName()
	{
		if (_condition.Clues == null && _condition.Items == null && _condition.Nodes == null)
		{
			_condition.gameObject.name = "<condition>";
			return;
		}
		else
		{
			_condition.gameObject.name = _condition.Clues.Length == 0 && _condition.Items.Length == 0 && _condition.Nodes.Length == 0 ? "<condition>" : "";
		}

		foreach (Clue clue in _condition.Clues)
		{
			if (clue != null)
				_condition.gameObject.name += (_condition.gameObject.name != "" ? " + " : "") + clue.Name;
		}

		foreach (Item item in _condition.Items)
		{
			if (item != null)
				_condition.gameObject.name += (_condition.gameObject.name != "" ? " + " : "") + item.Name;
		}

		foreach (Node node in _condition.Nodes)
		{
			if (node != null)
				_condition.gameObject.name += (_condition.gameObject.name != "" ? " + " : "") + "\"" + node.Text.Substring(0, node.Text.Length > 20 ? 20 : node.Text.Length) + (node.Text.Length > 20 ? "..." : "") + "\"";
		}

		if (_condition.gameObject.name == "")
		{
			_condition.gameObject.name = "<condition>";
		}
	}
}
