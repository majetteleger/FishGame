using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Option)), CanEditMultipleObjects]
public class OptionEditor : Editor
{
	private Option _option;

	void OnEnable()
	{
		_option = target as Option;
		EditorStyles.textArea.wordWrap = true;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Text", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_option.Text = EditorGUILayout.TextArea(_option.Text);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorManager.DefaultEditorLineHeight));
		EditorGUILayout.LabelField("Target Node", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_option.TargetNode = (Node)EditorGUILayout.ObjectField(_option.TargetNode, typeof(Node), true);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Return to Parent Node"))
		{
			Selection.activeObject = _option.GetComponentInParent<Node>();
		}

		DisplayName();
	}
	
	private void DisplayName()
	{
		_option.gameObject.name = (_option.Text == "" || _option.Text == null) ? "<option>" : "\"" + _option.Text.Substring(0, _option.Text.Length > 30 ? 30 : _option.Text.Length) + (_option.Text.Length > 30 ? "..." : "") + "\"";
	}
}
