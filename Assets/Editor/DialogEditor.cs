using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Dialog))]
public class DialogEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Dialog dialog = target as Dialog;

		EditorGUILayout.LabelField("Character");
		dialog.character = EditorGUILayout.TextField(dialog.character);
		
		dialog.gameObject.name = (dialog.name == "" || dialog.name == null) ? "<dialog>" : dialog.character + "(" + EditorSceneManager.GetActiveScene().name + ")";
	}
}