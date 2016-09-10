using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Dialog))]
public class DialogEditor : Editor
{
	private Dialog _dialog;

	void OnEnable()
	{
		_dialog = target as Dialog;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Title", GUILayout.Width(EditorManager.DefaultEditorLabelWidth));
		_dialog.Title = EditorGUILayout.TextField(_dialog.Title);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Add Node"))
		{
			GameObject newNode = new GameObject("<node>");
			Undo.RegisterCreatedObjectUndo(newNode, "Create " + newNode.name);
			newNode.AddComponent<Node>();
			newNode.transform.SetParent(_dialog.transform);
		}

		DisplayName();
	}

	private void DisplayName()
	{
		_dialog.gameObject.name = (_dialog.name == "" || _dialog.name == null) ? "<dialog>" : _dialog.Title + "(" + EditorSceneManager.GetActiveScene().name + ")";
	}
}